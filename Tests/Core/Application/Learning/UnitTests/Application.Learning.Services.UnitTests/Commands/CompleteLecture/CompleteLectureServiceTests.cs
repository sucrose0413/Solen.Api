using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.Services.UnitTests.Commands.CompleteLecture
{
    [TestFixture]
    public class CompleteLectureServiceTests
    {
        private Mock<ICompleteLectureRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CompleteLectureService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICompleteLectureRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CompleteLectureService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
        }

        [Test]
        public void CheckLectureExistence_LectureDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.DoesLectureExist("lectureId", "learningPathId", default))
                .ReturnsAsync(false);

            Assert.That(() => _sut.CheckLectureExistence("lectureId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void CheckLectureExistence_LectureDoesExist_ThrowNoException()
        {
            _repo.Setup(x => x.DoesLectureExist("lectureId", "learningPathId", default))
                .ReturnsAsync(true);

            Assert.That(() => _sut.CheckLectureExistence("lectureId", default), Throws.Nothing);
        }

        [Test]
        public void IsTheLectureAlreadyCompleted_LearnerHasNotCompletedTheLecture_ReturnFalse()
        {
            _repo.Setup(x => x.IsTheLectureAlreadyCompleted("lectureId", "learnerId", default))
                .ReturnsAsync(false);

            var result = _sut.IsTheLectureAlreadyCompleted("lectureId", default).Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsTheLectureAlreadyCompleted_LearnerHasCompletedTheLecture_ReturnTrue()
        {
            _repo.Setup(x => x.IsTheLectureAlreadyCompleted("lectureId", "learnerId", default))
                .ReturnsAsync(true);

            var result = _sut.IsTheLectureAlreadyCompleted("lectureId", default).Result;

            Assert.That(result, Is.True);
        }

        [Test]
        public void CreateCompletedLecture_WhenCalled_CreateCompletedLecture()
        {
            var result = _sut.CreateCompletedLecture("lectureId");

            Assert.That(result.LectureId, Is.EqualTo("lectureId"));
            Assert.That(result.LearnerId, Is.EqualTo("learnerId"));
        }

        [Test]
        public void AddLearnerCompletedLectureToRepo_WhenCalled_AddLearnerCompletedLectureToRepo()
        {
            var completedLecture = new LearnerCompletedLecture("learnerId", "lectureId");

            _sut.AddLearnerCompletedLectureToRepo(completedLecture);

            _repo.Verify(x => x.AddLearnerCompletedLecture(completedLecture));
        }
    }
}