using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.CoursesManagement.Edit.Services.LearningPaths;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.LearningPaths
{
    [TestFixture]
    public class UpdateCourseLearningPathsServiceTests
    {
        private Mock<IUpdateCourseLearningPathsRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateCourseLearningPathsService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateCourseLearningPathsRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateCourseLearningPathsService(_repo.Object, _currentUserAccessor.Object);
            
            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }
        
         [Test]
        public void GetCourseFromRepo_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourseWithLearningPaths("courseId", "organizationId", default))
                .ReturnsAsync((Course) null);

            Assert.That(() => _sut.GetCourseFromRepo("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseFromRepo_CourseDoesExist_ReturnCorrectCourse()
        {
            var course = new Course("title", "creatorId", DateTime.Now);
            _repo.Setup(x => x.GetCourseWithLearningPaths("courseId", "organizationId", default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseFromRepo("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public void CheckCourseStatusForModification_CourseNotEditable_ThrowUnalterableCourseException()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);

            Assert.That(() => _sut.CheckCourseStatusForModification(course.Object),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void CheckCourseStatusForModification_CourseIsEditable_NotThrowException()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);

            Assert.That(() => _sut.CheckCourseStatusForModification(course.Object), Throws.Nothing);
        }

        [Test]
        public void
            RemoveEventualLearningPaths_newLearningPathsIdsListContainsExistingLearningPaths_NotRemoveExistingLearningPaths()
        {
            // Arrange
            var course = new Course("title", "creatorId", DateTime.Now);
            var existingLearningPath = new LearningPathCourse("learningPathId1", "courseId", 1);
            course.AddLearningPath(existingLearningPath);

            var newCourseLearningPathsIds = new[] {"learningPathId1"};

            // Act
            _sut.RemoveEventualLearningPaths(course, newCourseLearningPathsIds);

            // Assert
            Assert.That(course.CourseLearningPaths, Has.Member(existingLearningPath));
        }

        [Test]
        public void
            RemoveEventualLearningPaths_newLearningPathsIdsListDoesNotContainExistingLearningPaths_RemoveExistingLearningPaths()
        {
            // Arrange
            var course = new Course("title", "creatorId", DateTime.Now);
            var existingLearningPath = new LearningPathCourse("learningPathId1", "courseId", 1);
            course.AddLearningPath(existingLearningPath);

            var newCourseLearningPathsIds = new[] {"learningPathId2"};

            // Act
            _sut.RemoveEventualLearningPaths(course, newCourseLearningPathsIds);

            // Assert
            Assert.That(course.CourseLearningPaths, Has.No.Member(existingLearningPath));
        }

        [Test]
        public void RemoveEventualLearningPaths_newLearningPathsIdsListIsEmpty_RemoveAllExistingLearningPaths()
        {
            // Arrange
            var course = new Course("title", "creatorId", DateTime.Now);
            var existingLearningPath1 = new LearningPathCourse("learningPathId1", "courseId", 1);
            var existingLearningPath2 = new LearningPathCourse("learningPathId2", "courseId", 2);
            course.AddLearningPath(existingLearningPath1);
            course.AddLearningPath(existingLearningPath2);

            var newCourseLearningPathsIds = new List<string>();

            // Act
            _sut.RemoveEventualLearningPaths(course, newCourseLearningPathsIds);

            // Assert
            Assert.That(course.CourseLearningPaths, Is.Empty);
        }

        [Test]
        public void GetLearningPathsIdsToAdd_NoNewLearningPathId_ReturnEmptyList()
        {
            // Arrange
            var course = new Course("title", "creatorId", DateTime.Now);
            var existingLearningPath = new LearningPathCourse("learningPathId1", "courseId", 1);
            course.AddLearningPath(existingLearningPath);

            var newCourseLearningPathsIds = new[] {"learningPathId1"};

            // Act
            var result = _sut.GetLearningPathsIdsToAdd(course, newCourseLearningPathsIds);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetLearningPathsIdsToAdd_NewLearningPathIdIsAdded_ShouldNotReturnEmptyList()
        {
            // Arrange
            var course = new Course("title", "creatorId", DateTime.Now);

            var newCourseLearningPathsIds = new[] {"newLearningPathId"};

            // Act
            var result = _sut.GetLearningPathsIdsToAdd(course, newCourseLearningPathsIds);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddLearningPathToCourse_LearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.DoesLearningPathExist("learningPathId", "organizationId", default))
                .ReturnsAsync(false);

            Assert.That(() => _sut.AddLearningPathToCourse(new Course("title", "creatorId", DateTime.Now), "learningPathId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void AddLearningPathToCourse_LearningPathDoesExist_AddTheNewLearningPathToTheCourseLearningPathsList()
        {
            // Arrange
            _repo.Setup(x => x.DoesLearningPathExist("learningPathId", "organizationId", default))
                .ReturnsAsync(true);
            _repo.Setup(x => x.GetLearningPathLastOrder("learningPathId", default))
                .ReturnsAsync(1);
            var course = new Course("title", "creatorId", DateTime.Now);

            // Act
            _sut.AddLearningPathToCourse(course, "learningPathId", default);

            // Assert
            Assert.That(course.CourseLearningPaths, 
                Has.Exactly(1).Matches<LearningPathCourse>(x =>
                x.CourseId == course.Id && x.LearningPathId == "learningPathId" && x.Order == 2));
        }
        
        [Test]
        public void UpdateCourseRepo_WhenCalled_UpdateCourseLearningPaths()
        {
            var course = new Course("title", "creatorId", DateTime.Now);

            _sut.UpdateCourseRepo(course);

            _repo.Verify(x => x.UpdateCourseLearningPaths(course));
        }
    }
}