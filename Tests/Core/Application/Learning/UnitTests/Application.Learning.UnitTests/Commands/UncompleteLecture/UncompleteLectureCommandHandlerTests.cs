using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.UnitTests.Commands.UncompleteLecture
{
    [TestFixture]
    public class UncompleteLectureCommandHandlerTests
    {
        private UncompleteLectureCommandHandler _sut;
        private Mock<IUncompleteLectureService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UncompleteLectureCommand _command;

        private LearnerCompletedLecture _completedLecture;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUncompleteLectureService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UncompleteLectureCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UncompleteLectureCommand("lectureId");
            
            _completedLecture = new LearnerCompletedLecture("learnerId", _command.LectureId);
            _service.Setup(x => x.GetCompletedLecture(_command.LectureId, default))
                .ReturnsAsync(_completedLecture);
        }


        [Test]
        public void WhenCalled_TheLectureIsAlreadyUncompleted_DoNotPerformSaveMethod()
        {
            _service.Setup(x => x.GetCompletedLecture(_command.LectureId, default))
                .ReturnsAsync((LearnerCompletedLecture) null);

            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Never);
        }

        [Test]
        public void WhenCalled_RemoveLearnerCompletedLectureFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveLearnerCompletedLectureFromRepo(_completedLecture));
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
                _service.Setup(x => x.RemoveLearnerCompletedLectureFromRepo(_completedLecture))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}