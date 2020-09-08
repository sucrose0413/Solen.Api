using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.UnitTests.Commands.CompleteLecture
{
    [TestFixture]
    public class CompleteLectureCommandHandlerTests
    {
        private CompleteLectureCommandHandler _sut;
        private Mock<ICompleteLectureService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private CompleteLectureCommand _command;

        private LearnerCompletedLecture _completedLecture;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICompleteLectureService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new CompleteLectureCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new CompleteLectureCommand {LectureId = "lectureId"};


            _service.Setup(x => x.IsTheLectureAlreadyCompleted(_command.LectureId, default))
                .ReturnsAsync(false);

            _completedLecture = new LearnerCompletedLecture("learnerId", _command.LectureId);
            _service.Setup(x => x.CreateCompletedLecture(_command.LectureId))
                .Returns(_completedLecture);
        }

        [Test]
        public void WhenCalled_CheckLectureExistence()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckLectureExistence(_command.LectureId, default));
        }

        [Test]
        public void WhenCalled_TheLectureIsAlreadyCompleted_DoNotPerformSaveMethod()
        {
            _service.Setup(x => x.IsTheLectureAlreadyCompleted(_command.LectureId, default))
                .ReturnsAsync(true);

            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Never);
        }

        [Test]
        public void WhenCalled_AddLearnerCompletedLectureToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddLearnerCompletedLectureToRepo(_completedLecture));
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
                _service.Setup(x => x.CheckLectureExistence(_command.LectureId, default))
                    .InSequence();
                _service.Setup(x => x.IsTheLectureAlreadyCompleted(_command.LectureId, default))
                    .InSequence().ReturnsAsync(false);
                _service.Setup(x => x.AddLearnerCompletedLectureToRepo(_completedLecture))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}