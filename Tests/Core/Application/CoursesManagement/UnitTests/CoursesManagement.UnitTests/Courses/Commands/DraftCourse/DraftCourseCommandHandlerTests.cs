using System;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.DraftCourse
{
    [TestFixture]
    public class DraftCourseCommandHandlerTests
    {
        private Mock<IDraftCourseService> _service;
        private Mock<ICoursesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private DraftCourseCommand _command;
        private DraftCourseCommandHandler _sut;
        private Course _courseToDraft;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IDraftCourseService>();
            _commonService = new Mock<ICoursesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new DraftCourseCommand {CourseId = "courseId"};
            _sut = new DraftCourseCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object);


            _courseToDraft = new Course("name", "creatorId", DateTime.Now);
            _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToDraft);
        }

        [Test]
        public void ControlIsOk_ChangeCourseStatusToDraft()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ChangeTheCourseStatusToDraft(_courseToDraft));
        }

        [Test]
        public void ControlIsOk_UpdateCourseRepo()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.UpdateCourseRepo(_courseToDraft));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.ChangeTheCourseStatusToDraft(_courseToDraft)).InSequence();
                _commonService.Setup(x => x.UpdateCourseRepo(_courseToDraft)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}