using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Application.Learning.Services.UnitTests.Commands.AddLearnerAccessHistory
{
    [TestFixture]
    public class AddLearnerAccessHistoryServiceTests
    {
        private Mock<IAddLearnerAccessHistoryRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private AddLearnerAccessHistoryService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IAddLearnerAccessHistoryRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new AddLearnerAccessHistoryService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
        }

        [Test]
        public void GetLectureCourseId_LectureDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLectureCourseId("lectureId", "learningPathId", default))
                .ReturnsAsync((string) null);

            Assert.That(() => _sut.GetLectureCourseId("lectureId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetLectureCourseId_LectureDoesExist_ReturnCorrectCourseId()
        {
            var courseId = "courseId";
            _repo.Setup(x => x.GetLectureCourseId("lectureId", "learningPathId", default))
                .ReturnsAsync(courseId);

            var result = _sut.GetLectureCourseId("lectureId", default).Result;

            Assert.That(result, Is.EqualTo(courseId));
        }

        [Test]
        public void CreateAccessHistory_WhenCalled_CreateLearnerLectureAccessHistory()
        {
            var result = _sut.CreateAccessHistory("lectureId");

            Assert.That(result.LearnerId, Is.EqualTo("learnerId"));
            Assert.That(result.LectureId, Is.EqualTo("lectureId"));
        }

        [Test]
        public void AddLearnerLectureAccessHistoryToRepo_WhenCalled_AddLearnerLectureAccessHistoryToRepo()
        {
            var accessHistory = new LearnerLectureAccessHistory("learnerId", "lectureId");

            _sut.AddLearnerLectureAccessHistoryToRepo(accessHistory);

            _repo.Verify(x => x.AddLearnerLectureAccessHistory(accessHistory));
        }

        [Test]
        public void UpdateOrCreateLearnerCourseAccessTime_TheLearnerHasNeverAccessedToTheCourse_CreateNewCourseAccess()
        {
            _repo.Setup(x => x.GetLearnerCourseAccessTime("learnerId", "courseId", default))
                .ReturnsAsync((LearnerCourseAccessTime) null);

            _sut.UpdateOrCreateLearnerCourseAccessTime("courseId", default).Wait();

            _repo.Verify(x => x.AddLearnerCourseAccessTime(
                It.Is<LearnerCourseAccessTime>(l => l.CourseId == "courseId"
                                                    && l.LearnerId == "learnerId")));
        }

        [Test]
        public void UpdateOrCreateLearnerCourseAccessTime_TheLearnerHasAlreadyAccessedToTheCourse_UpdateExistingAccess()
        {
            var accessTime = new LearnerCourseAccessTime("learnerId", "courseId");
            _repo.Setup(x => x.GetLearnerCourseAccessTime("learnerId", "courseId", default))
                .ReturnsAsync(accessTime);

            _sut.UpdateOrCreateLearnerCourseAccessTime("courseId", default).Wait();

            _repo.Verify(x => x.UpdateLearnerCourseAccessTime(accessTime));
        }
    }
}