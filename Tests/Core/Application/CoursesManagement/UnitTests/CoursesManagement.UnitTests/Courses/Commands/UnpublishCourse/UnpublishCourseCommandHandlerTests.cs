using System;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.UnpublishCourse
{
    [TestFixture]
    public class UnpublishCourseCommandHandlerTests
    {
        private Mock<IUnpublishCourseService> _service;
        private Mock<ICoursesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UnpublishCourseCommand _command;
        private UnpublishCourseCommandHandler _sut;
        private Course _courseToUnpublish;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUnpublishCourseService>();
            _commonService = new Mock<ICoursesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new UnpublishCourseCommand {CourseId = "courseId"};
            _sut = new UnpublishCourseCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object);


            _courseToUnpublish = new Course("name", "creatorId", DateTime.Now);
            _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToUnpublish);
        }

        [Test]
        public void WhenCalled_ChangeCourseStatusToUnpublished()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ChangeTheCourseStatusToUnpublished(_courseToUnpublish));
        }

        [Test]
        public void ControlIsOk_UpdateCourseRepo()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.UpdateCourseRepo(_courseToUnpublish));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.ChangeTheCourseStatusToUnpublished(_courseToUnpublish))
                    .InSequence();
                _commonService.Setup(x => x.UpdateCourseRepo(_courseToUnpublish)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}