using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.Services.UnitTests.Commands.UpdateCoursesOrders
{
    [TestFixture]
    public class UpdateCoursesOrdersServiceTests
    {
        private Mock<IUpdateCoursesOrdersRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateCoursesOrdersService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateCoursesOrdersRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateCoursesOrdersService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPathCourses_WhenCalled_ReturnLearningPathCourses()
        {
            var learningPathCourses = new List<LearningPathCourse>();
            _repo.Setup(x => x.GetLearningPathCourses("learningPathId", "organizationId", default))
                .ReturnsAsync(learningPathCourses);

            var result = _sut.GetLearningPathCourses("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(learningPathCourses));
        }

        [Test]
        public void UpdateCoursesOrders_WhenCalled_UpdateCoursesOrders()
        {
            // Arrange
            var course1 = new LearningPathCourse("learningPathId", "course1", 1);
            var course2 = new LearningPathCourse("learningPathId", "course2", 2);
            var coursesToUpdateOrders = new List<LearningPathCourse> {course1, course2};

            var coursesNewOrders = new[]
            {
                new CourseOrderDto {CourseId = course1.CourseId, Order = 2},
                new CourseOrderDto {CourseId = course2.CourseId, Order = 1}
            };

            // Act
            _sut.UpdateCoursesOrders(coursesToUpdateOrders, coursesNewOrders);

            // Assert
            Assert.That(course1.Order, Is.EqualTo(2));
            Assert.That(course2.Order, Is.EqualTo(1));
        }

        [Test]
        public void UpdateLearningPathCoursesRepo_WhenCalled_UpdateLearningPathCoursesRepo()
        {
            var learningPathCourses = new List<LearningPathCourse>();

            _sut.UpdateLearningPathCoursesRepo(learningPathCourses);

            _repo.Verify(x => x.UpdateLearningPathCourses(learningPathCourses));
        }
    }
}