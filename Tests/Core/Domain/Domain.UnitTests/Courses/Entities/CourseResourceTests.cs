using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class CourseResourceTests
    {
        private CourseResource _sut;
        
        [Test]
        public void ConstructorWithCourseIdModuleIdLectureIdResourceId_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new CourseResource("courseId", "moduleId", "lectureId", "resourceId");

            Assert.That(_sut.CourseId, Is.EqualTo("courseId"));
            Assert.That(_sut.ModuleId, Is.EqualTo("moduleId"));
            Assert.That(_sut.LectureId, Is.EqualTo("lectureId"));
            Assert.That(_sut.ResourceId, Is.EqualTo("resourceId"));
        }
    }
}