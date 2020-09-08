using System.Linq;
using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class LearningPathTests
    {
        private LearningPath _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LearningPath("learning path", "organizationId");
        }

        [Test]
        public void ConstructorWithNameOrganizationId_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new LearningPath("learning path", "organizationId");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Name, Is.EqualTo("learning path"));
            Assert.That(_sut.OrganizationId, Is.EqualTo("organizationId"));
        }

        [Test]
        public void UpdateName_WhenCalled_UpdateLearningPathName()
        {
            _sut.UpdateName("new name");

            Assert.That(_sut.Name, Is.EqualTo("new name"));
        }

        [Test]
        public void UpdateDescription_WhenCalled_UpdateLearningPathDescription()
        {
            _sut.UpdateDescription("new description");

            Assert.That(_sut.Description, Is.EqualTo("new description"));
        }

        [Test]
        public void AddCourse_TheLearningPathHasNoCourse_AddTheCourseToLearningPathCoursesListWithOrder1()
        {
            _sut.AddCourse("courseId");

            Assert.That(_sut.LearningPathCourses.Count(x => x.CourseId == "courseId" && x.Order == 1),
                Is.EqualTo(1));
        }

        [Test]
        public void AddCourse_TheLearningPathHasCourseS_AddTheCourseToLearningPathCoursesListWithNextOrder()
        {
            _sut.AddCourse("existingCourseId");
            _sut.AddCourse("newCourseId");

            Assert.That(_sut.LearningPathCourses.Count(x => x.CourseId == "newCourseId" && x.Order == 2),
                Is.EqualTo(1));
        }

        [Test]
        public void RemoveRole_WhenCalled_RemoveTheCourseFromLearningPathCoursesList()
        {
            _sut.AddCourse("courseId");

            _sut.RemoveCourse("courseId");

            Assert.That(_sut.LearningPathCourses.Count(x => x.CourseId == "courseId"),
                Is.EqualTo(0));
        }
    }
}