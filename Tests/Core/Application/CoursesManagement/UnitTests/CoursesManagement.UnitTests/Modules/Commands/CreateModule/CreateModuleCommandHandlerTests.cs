using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Modules.Commands.CreateModule
{
    [TestFixture]
    public class CreateModuleCommandHandlerTests
    {
        private Mock<ICreateModuleService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private CreateModuleCommand _command;
        private CreateModuleCommandHandler _sut;

        private Module _createdModule;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICreateModuleService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new CreateModuleCommand
                {Name = "module name", CourseId = "courseId", Order = 1};

            _sut = new CreateModuleCommandHandler(_service.Object, _unitOfWork.Object);

            _createdModule = new Module("module", "courseId", 1);
            _service.Setup(x => x.CreateModule(_command)).Returns(_createdModule);
        }

        [Test]
        public void WhenCalled_ControlCourseExistenceAndStatus()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ControlCourseExistenceAndStatus(_command.CourseId, default));
        }

        [Test]
        public void WhenCalled_CreateTheModule()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CreateModule(_command));
        }

        [Test]
        public void ControlIsOk_AddModuleToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddModuleToRepo(_createdModule, default));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _unitOfWork.Setup(x => x.SaveAsync(default));

            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void SaveChangesIsOk_ReturnCreatedModuleId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_createdModule.Id));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.ControlCourseExistenceAndStatus(_command.CourseId, default)).InSequence();
                _service.Setup(x => x.CreateModule(_command)).InSequence().Returns(_createdModule);
                _service.Setup(x => x.AddModuleToRepo(_createdModule, default)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}