using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Lectures.Commands.UpdateLecture
{
    [TestFixture]
    public class UpdateLectureCommandHandlerTests
    {
        private Mock<IUpdateLectureService> _service;
        private Mock<ILecturesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateLectureCommand _command;
        private UpdateLectureCommandHandler _sut;
        private Lecture _lectureToUpdate;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateLectureService>();
            _commonService = new Mock<ILecturesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new UpdateLectureCommand {LectureId = "lectureId"};
            _sut = new UpdateLectureCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object);


            _lectureToUpdate = new ArticleLecture("title", "moduleId", 1, "content");
            _commonService.Setup(x => x.GetLectureFromRepo(_command.LectureId, default))
                .ReturnsAsync(_lectureToUpdate);
        }

        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_lectureToUpdate));
        }

        [Test]
        public void ControlIsOk_UpdateLectureTitle()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLectureTitle(_lectureToUpdate, _command.Title));
        }

        [Test]
        public void ControlIsOk_UpdateLectureDuration()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLectureDuration(_lectureToUpdate, _command.Duration));
        }

        [Test]
        public void ControlIsOkAndLectureIsArticle_UpdateLectureContent()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateArticleContent((ArticleLecture) _lectureToUpdate, _command.Content));
        }


        [Test]
        public void ControlIsOk_UpdateLectureRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLectureRepo(_lectureToUpdate));
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
                _commonService.Setup(x => x.CheckCourseStatusForModification(_lectureToUpdate)).InSequence();
                _service.Setup(x => x.UpdateLectureTitle(_lectureToUpdate, _command.Title)).InSequence();
                _service.Setup(x => x.UpdateLectureDuration(_lectureToUpdate, _command.Duration)).InSequence();
                _service.Setup(x => x.UpdateLectureRepo(_lectureToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}