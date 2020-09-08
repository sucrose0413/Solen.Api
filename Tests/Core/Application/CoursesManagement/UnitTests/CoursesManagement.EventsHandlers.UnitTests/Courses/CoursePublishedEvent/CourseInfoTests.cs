using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;

namespace CoursesManagement.EventsHandlers.UnitTests.Courses
{
    [TestFixture]
    public class CourseInfoTests
    {
        private CourseInfo _sut;

        [Test]
        public void ConstructorWithCourseNameAndCreator_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new CourseInfo("course name", "creator name");

            Assert.That(_sut.CourseName, Is.EqualTo("course name"));
            Assert.That(_sut.CreatorName, Is.EqualTo("creator name"));
        }
    }
}