using System;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.UpdateCourse
{
    [TestFixture]
    public class UpdateCourseCommandHandlerTests
    {
        private Mock<IUpdateCourseService> _service;
        private Mock<ICoursesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateCourseCommand _command;
        private UpdateCourseCommandHandler _sut;
        private Course _courseToUpdate;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateCourseService>();
            _commonService = new Mock<ICoursesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new UpdateCourseCommand() {CourseId = "courseId"};
            _sut = new UpdateCourseCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object);

            _courseToUpdate = new Course("title", "creatorId", DateTime.Now);
            _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToUpdate);
        }

        [Test]
        public void WhenCalled_ControlCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_courseToUpdate));
        }

        [Test]
        public void ControlIsOk_UpdateCourseTitle()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCourseTitle(_courseToUpdate, _command.Title));
        }

        [Test]
        public void ControlIsOk_UpdateCourseSubtitle()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCourseSubtitle(_courseToUpdate, _command.Subtitle));
        }

        [Test]
        public void ControlIsOk_UpdateCourseDescription()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCourseDescription(_courseToUpdate, _command.Description));
        }
        
        [Test]
        public void ControlIsOk_RemoveExistingCourseLearnedSkills()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveCourseSkillsFromRepo(_courseToUpdate.Id, default));
        }

        [Test]
        public void ControlIsOk_AddCourseLearnedSkills()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddSkillsToCourse(_courseToUpdate, _command.CourseLearnedSkills));
        }

        [Test]
        public void ControlIsOk_UpdateCourseRepo()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.UpdateCourseRepo(_courseToUpdate));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _unitOfWork.Setup(x => x.SaveAsync(default));

            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _commonService.Setup(x => x.CheckCourseStatusForModification(_courseToUpdate)).InSequence();
                _service.Setup(x => x.UpdateCourseTitle(_courseToUpdate, _command.Title)).InSequence();
                _service.Setup(x => x.UpdateCourseSubtitle(_courseToUpdate, _command.Subtitle)).InSequence();
                _service.Setup(x => x.UpdateCourseDescription(_courseToUpdate, _command.Description)).InSequence();
                _service.Setup(x => x.RemoveCourseSkillsFromRepo(_courseToUpdate.Id, default)).InSequence();
                _service.Setup(x => x.AddSkillsToCourse(_courseToUpdate, _command.CourseLearnedSkills)).InSequence();
                _commonService.Setup(x => x.UpdateCourseRepo(_courseToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}