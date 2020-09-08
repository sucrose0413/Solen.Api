using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Modules.Commands.UpdateLecturesOrders
{
    [TestFixture]
    public class UpdateLecturesOrdersCommandHandlerTests
    {
        private Mock<IUpdateLecturesOrdersService> _service;
        private Mock<IModulesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateLecturesOrdersCommand _command;
        private List<Lecture> _lecturesToUpdateOrders;
        private UpdateLecturesOrdersCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateLecturesOrdersService>();
            _commonService = new Mock<IModulesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new UpdateLecturesOrdersCommand();

            _sut = new UpdateLecturesOrdersCommandHandler(_service.Object, _commonService.Object,
                _unitOfWork.Object);

            _lecturesToUpdateOrders = new List<Lecture>();
            _service.Setup(x => x.GetModuleLecturesFromRepo(_command.ModuleId, default))
                .ReturnsAsync(_lecturesToUpdateOrders);
        }

        [Test]
        public void WhenCalled_ControlModuleExistenceAndCourseStatus()
        {
            var module = new Module("title", "courseId", 1);
            _commonService.Setup(x => x.GetModuleFromRepo(_command.ModuleId, default)).ReturnsAsync(module);

            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(module));
        }

        [Test]
        public void ControlsAreOk_UpdateLecturesOrder()
        {
            _sut.Handle(_command, default).Wait();

            _service.Setup(x => x.UpdateLecturesOrders(_lecturesToUpdateOrders, _command.LecturesOrders));
        }

        [Test]
        public void ControlsAreOk_UpdateLecturesRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLecturesRepo(_lecturesToUpdateOrders));
        }

        [Test]
        public void ControlsAreOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                var module = new Module("title", "courseId", 1);
                _commonService.Setup(x => x.GetModuleFromRepo(_command.ModuleId, default)).InSequence()
                    .ReturnsAsync(module);
                _commonService.Setup(x => x.CheckCourseStatusForModification(module)).InSequence();
                _service.Setup(x => x.GetModuleLecturesFromRepo(_command.ModuleId, default)).InSequence()
                    .ReturnsAsync(_lecturesToUpdateOrders);
                _service.Setup(x => x.UpdateLecturesOrders(_lecturesToUpdateOrders, _command.LecturesOrders))
                    .InSequence();
                _service.Setup(x => x.UpdateLecturesRepo(_lecturesToUpdateOrders)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}