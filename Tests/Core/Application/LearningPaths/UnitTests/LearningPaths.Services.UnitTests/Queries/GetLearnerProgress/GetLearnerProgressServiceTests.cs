using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Services.Queries;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;

namespace LearningPaths.Services.UnitTests.Queries.GetLearnerProgress
{
    [TestFixture]
    public class GetLearnerProgressServiceTests
    {
        private Mock<IGetLearnerProgressRepository> _repo;
        private Mock<IUserManager> _userManager;
        private GetLearnerProgressService _sut;
        
        private readonly string _publishedStatus = new PublishedStatus().Name;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearnerProgressRepository>();
            _userManager = new Mock<IUserManager>();
            _sut = new GetLearnerProgressService(_repo.Object, _userManager.Object);
        }

        [Test]
        public void GetLearner_LearnerDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByIdAsync("learnerId"))
                .ReturnsAsync((User) null);


            Assert.That(() => _sut.GetLearner("learnerId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetLearner_LearnerDoesExist_ReturnLearner()
        {
            var learner = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("learnerId")).ReturnsAsync(learner);

            var result = _sut.GetLearner("learnerId", default).Result;

            Assert.That(result, Is.EqualTo(learner));
        }

        [Test]
        public void GetLearnerCompletedCourses_LearnerHasNoLearningPath_ReturnEmptyObject()
        {
            var learner = new User("email", "organizationId");
            learner.UpdateLearningPath(null);

            var result = _sut.GetLearnerCompletedCourses(learner, default).Result;

            Assert.That(result.CompletedCourses, Is.Empty);
            Assert.That(result.LearningPathCourseCount, Is.EqualTo(0));
        }

        [Test]
        public void GetLearnerCompletedCourses_LearnerHasLearningPath_ReturnLearnerCompletedCourses()
        {
            // Arrange
            var learner = new User("email", "organizationId");
            var learningPath = new LearningPath("learning path", "organizationId");
            learner.UpdateLearningPath(learningPath);
            var course1 = new LearningPathCourseDto("courseId1", "title", 1);
            var course2 = new LearningPathCourseDto("courseId2", "title", 1);
            var publishedCourses = new List<LearningPathCourseDto> {course1, course2};
            _repo.Setup(x => x.GetLearningPathPublishedCourses(learningPath.Id, _publishedStatus, default))
                .ReturnsAsync(publishedCourses);
            _repo.Setup(x => x.GetLearnerCompletedLectures(learner.Id, "courseId1", _publishedStatus, default))
                .ReturnsAsync(1);
            _repo.Setup(x => x.GetLearnerCompletedLectures(learner.Id, "courseId2", _publishedStatus, default))
                .ReturnsAsync(0);

            // Act
            var result = _sut.GetLearnerCompletedCourses(learner, default).Result;

            // Assert
            Assert.That(result.CompletedCourses.Count, Is.EqualTo(1));
            Assert.That(result.LearningPathCourseCount, Is.EqualTo(2));
        }
    }
}