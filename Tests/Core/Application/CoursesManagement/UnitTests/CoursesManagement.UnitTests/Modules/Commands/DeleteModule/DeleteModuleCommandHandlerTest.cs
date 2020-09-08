using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Modules.Commands.DeleteModule
{
    [TestFixture]
    public class DeleteModuleCommandHandlerTest
    {
        private Mock<IDeleteModuleService> _service;
        private Mock<IModulesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private DeleteModuleCommand _command;
        private DeleteModuleCommandHandler _sut;
        private Module _moduleToDelete;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IDeleteModuleService>();
            _commonService = new Mock<IModulesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _command = new DeleteModuleCommand {ModuleId = "moduleId"};
            _sut = new DeleteModuleCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object,
                _mediator.Object);


            _moduleToDelete = new Module("name", "courseId", 1);
            _commonService.Setup(x => x.GetModuleFromRepo(_command.ModuleId, default))
                .ReturnsAsync(_moduleToDelete);
        }

        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_moduleToDelete));
        }

        [Test]
        public void ControlIsOk_RemoveModuleToDeleteFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveModuleFromRepo(_moduleToDelete));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void ModuleDeleted_SendModuleDeletedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<ModuleDeletedEvent>(), default));
        }

        [Test]
        public void ModuleDeleted_ReturnModuleDeletedId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_moduleToDelete.Id));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _commonService.Setup(x => x.CheckCourseStatusForModification(_moduleToDelete)).InSequence();
                _service.Setup(x => x.RemoveModuleFromRepo(_moduleToDelete)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<ModuleDeletedEvent>(), default))
                    .InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}