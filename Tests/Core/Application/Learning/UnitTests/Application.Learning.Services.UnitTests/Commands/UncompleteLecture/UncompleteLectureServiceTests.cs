using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.Services.UnitTests.Commands.UncompleteLecture
{
    [TestFixture]
    public class UncompleteLectureServiceTests
    {
        private Mock<IUncompleteLectureRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UncompleteLectureService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUncompleteLectureRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UncompleteLectureService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
        }

        [Test]
        public void GetCompletedLecture_WhenCalled_ReturnCorrectCompletedLecture()
        {
            var completedLecture = new LearnerCompletedLecture("learnerId", "lectureId");
            _repo.Setup(x => x.GetCompletedLecture("learnerId", "lectureId", default))
                .ReturnsAsync(completedLecture);

            var result = _sut.GetCompletedLecture("lectureId", default).Result;

            Assert.That(result, Is.EqualTo(completedLecture));
        }

        [Test]
        public void RemoveLearnerCompletedLectureFromRepo_WhenCalled_RemoveLearnerCompletedLectureFromRepo()
        {
            var completedLecture = new LearnerCompletedLecture("learnerId", "lectureId");

            _sut.RemoveLearnerCompletedLectureFromRepo(completedLecture);

            _repo.Verify(x => x.RemoveLearnerCompletedLecture(completedLecture));
        }
    }
}