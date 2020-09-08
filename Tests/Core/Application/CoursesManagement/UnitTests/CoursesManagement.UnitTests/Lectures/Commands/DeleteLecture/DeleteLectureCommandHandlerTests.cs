using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Lectures.Commands.DeleteLecture
{
    [TestFixture]
    public class DeleteLectureCommandHandlerTests
    {
        private Mock<IDeleteLectureService> _service;
        private Mock<ILecturesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private DeleteLectureCommand _command;
        private DeleteLectureCommandHandler _sut;
        private Lecture _lectureToDelete;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IDeleteLectureService>();
            _commonService = new Mock<ILecturesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _command = new DeleteLectureCommand {LectureId = "lectureId"};
            _sut = new DeleteLectureCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object,
                _mediator.Object);


            _lectureToDelete = new VideoLecture("title", "moduleId", 1);
            _commonService.Setup(x => x.GetLectureFromRepo(_command.LectureId, default))
                .ReturnsAsync(_lectureToDelete);
        }

        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_lectureToDelete));
        }

        [Test]
        public void ControlIsOk_RemoveLectureToDeleteFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveLectureFromRepo(_lectureToDelete));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void LectureDeleted_SendLectureDeletedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<LectureDeletedEvent>(), default));
        }


        [Test]
        public void SaveChangesIsOk_ReturnDeletedLectureId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_lectureToDelete.Id));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _commonService.Setup(x => x.CheckCourseStatusForModification(_lectureToDelete)).InSequence();
                _service.Setup(x => x.RemoveLectureFromRepo(_lectureToDelete)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<LectureDeletedEvent>(), default))
                    .InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}