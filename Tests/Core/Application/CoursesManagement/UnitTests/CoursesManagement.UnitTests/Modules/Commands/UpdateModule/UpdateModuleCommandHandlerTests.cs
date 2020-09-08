using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Modules.Commands.UpdateModule
{
    [TestFixture]
    public class UpdateModuleCommandHandlerTests
    {
        private Mock<IUpdateModuleService> _service;
        private Mock<IModulesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateModuleCommand _command;
        private UpdateModuleCommandHandler _sut;
        private Module _moduleToUpdate;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateModuleService>();
            _commonService = new Mock<IModulesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new UpdateModuleCommand() {ModuleId = "moduleId"};
            _sut = new UpdateModuleCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object);

            _moduleToUpdate = new Module("module", "courseId", 1);
            _commonService.Setup(x => x.GetModuleFromRepo(_command.ModuleId, default))
                .ReturnsAsync(_moduleToUpdate);
        }

        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_moduleToUpdate));
        }

        [Test]
        public void ControlIsOk_UpdateModuleName()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateModuleName(_moduleToUpdate, _command.Name));
        }


        [Test]
        public void ControlIsOk_UpdateModuleRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateModuleRepo(_moduleToUpdate));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _commonService.Setup(x => x.CheckCourseStatusForModification(_moduleToUpdate)).InSequence();
                _service.Setup(x => x.UpdateModuleName(_moduleToUpdate, _command.Name)).InSequence();
                _service.Setup(x => x.UpdateModuleRepo(_moduleToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}