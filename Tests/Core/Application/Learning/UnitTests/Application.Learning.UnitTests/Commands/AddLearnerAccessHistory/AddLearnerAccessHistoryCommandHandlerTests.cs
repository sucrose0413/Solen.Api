using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.UnitTests.Commands.AddLearnerAccessHistory
{
    [TestFixture]
    public class AddLearnerAccessHistoryCommandHandlerTests
    {
        private AddLearnerAccessHistoryCommandHandler _sut;
        private Mock<IAddLearnerAccessHistoryService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private AddLearnerAccessHistoryCommand _command;

        private string _courseId;
        private LearnerLectureAccessHistory _accessHistory;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IAddLearnerAccessHistoryService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new AddLearnerAccessHistoryCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new AddLearnerAccessHistoryCommand {LectureId = "lectureId"};

            _courseId = "courseId";
            _service.Setup(x => x.GetLectureCourseId(_command.LectureId, default))
                .ReturnsAsync(_courseId);

            _accessHistory = new LearnerLectureAccessHistory("learnerId", "lectureId");
            _service.Setup(x => x.CreateAccessHistory(_command.LectureId))
                .Returns(_accessHistory);
        }

        [Test]
        public void WhenCalled_UpdateOrCreateLearnerCourseAccessTime()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateOrCreateLearnerCourseAccessTime(_courseId, default));
        }

        [Test]
        public void WhenCalled_AddLearnerLectureAccessHistoryToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddLearnerLectureAccessHistoryToRepo(_accessHistory));
        }

        [Test]
        public void WhenCalled_SaveTheChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.UpdateOrCreateLearnerCourseAccessTime(_courseId, default))
                    .InSequence();
                _service.Setup(x => x.AddLearnerLectureAccessHistoryToRepo(_accessHistory))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}