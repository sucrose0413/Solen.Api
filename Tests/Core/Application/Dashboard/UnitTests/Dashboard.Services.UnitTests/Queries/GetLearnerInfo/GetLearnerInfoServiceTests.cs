using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Dashboard.Services.UnitTests.Queries.GetLearnerInfo
{
    [TestFixture]
    public class GetLearnerInfoServiceTests
    {
        private Mock<IGetLearnerInfoRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetLearnerInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearnerInfoRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetLearnerInfoService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
        }

        [Test]
        public void GetLastCourse_LastAccessedCourseIsNotNull_ReturnLastAccessedCourse()
        {
            var lastCourse = new Course("course", "creator", DateTime.Now);
            _repo.Setup(x => x.GetLastAccessedCourse("learnerId", default))
                .ReturnsAsync(lastCourse);

            var result = _sut.GetLastCourse(default).Result;

            Assert.That(result, Is.EqualTo(lastCourse));
        }

        [Test]
        public void GetLastCourse_LastAccessedCourseIsNull_ReturnLearningPathFirstCourse()
        {
            _repo.Setup(x => x.GetLastAccessedCourse("learnerId", default))
                .ReturnsAsync((Course) null);
            var learningPathFirstCourse = new Course("course", "creator", DateTime.Now);
            _repo.Setup(x => x.GetLearningPathFirstCourse("learningPathId", default))
                .ReturnsAsync(learningPathFirstCourse);

            var result = _sut.GetLastCourse(default).Result;

            Assert.That(result, Is.EqualTo(learningPathFirstCourse));
        }

        [Test]
        public void GetLastCourseProgress_LastCourseIsNull_ReturnNull()
        {
            var result = _sut.GetLastCourseProgress(null, default).Result;

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetLastCourseProgress_LastCourseIsNotNull_ReturnLastCourseProgress()
        {
            // Arrange
            var lastCourse = new Course("course", "creator", DateTime.Now);
            var totalDuration = 10;
            _repo.Setup(x => x.GetCourseTotalDuration(lastCourse.Id, default))
                .ReturnsAsync(totalDuration);
            var completedDuration = 1;
            _repo.Setup(x => x.GetLearnerCompletedDuration("learnerId", lastCourse.Id, default))
                .ReturnsAsync(completedDuration);

            // Act
            var result = _sut.GetLastCourseProgress(lastCourse, default).Result;

            // Assert
            Assert.That(result.CourseId, Is.EqualTo(lastCourse.Id));
            Assert.That(result.CourseTitle, Is.EqualTo(lastCourse.Title));
            Assert.That(result.CompletedDuration, Is.EqualTo(completedDuration));
            Assert.That(result.TotalDuration, Is.EqualTo(totalDuration));
        }
    }
}