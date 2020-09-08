using System;
using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.UpdateModulesOrders
{
    [TestFixture]
    public class UpdateModulesOrdersCommandHandlerTests
    {
        private Mock<IUpdateModulesOrdersService> _service;
        private Mock<ICoursesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateModulesOrdersCommand _command;
        private List<Module> _modulesToUpdateOrders;
        private UpdateModulesOrdersCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateModulesOrdersService>();
            _commonService = new Mock<ICoursesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();


            _command = new UpdateModulesOrdersCommand
            {
                CourseId = "courseId",
                ModulesOrders = new[] {new ModuleOrderDto()}
            };
            _modulesToUpdateOrders = new List<Module>();

            _sut = new UpdateModulesOrdersCommandHandler(_service.Object, _commonService.Object,
                _unitOfWork.Object);

            _service.Setup(x => x.GetCourseModulesFromRepo(_command.CourseId, default))
                .ReturnsAsync(_modulesToUpdateOrders);
        }

        [Test]
        public void WhenCalled_ControlCourseExistenceAndStatus()
        {
            var course = new Course("title", "creatorId", DateTime.Now);
            _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default)).ReturnsAsync(course);

            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(course));
        }


        [Test]
        public void ControlsAreOk_UpdateModulesOrder()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateModulesOrders(_modulesToUpdateOrders, _command.ModulesOrders));
        }

        [Test]
        public void ControlsAreOk_UpdateModulesRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateModulesRepo(_modulesToUpdateOrders));
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
                var course = new Course("title", "creatorId", DateTime.Now);
                _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default)).InSequence()
                    .ReturnsAsync(course);
                _commonService.Setup(x => x.CheckCourseStatusForModification(course)).InSequence();
                _service.Setup(x => x.GetCourseModulesFromRepo(_command.CourseId, default)).InSequence()
                    .ReturnsAsync(_modulesToUpdateOrders);
                _service.Setup(x => x.UpdateModulesOrders(_modulesToUpdateOrders, _command.ModulesOrders))
                    .InSequence();
                _service.Setup(x => x.UpdateModulesRepo(_modulesToUpdateOrders)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}