using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class LearnerCourseAccessTimeTests
    {
        private LearnerCourseAccessTime _sut;

        [Test]
        public void ConstructorWithLearnerIdCourseId_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new LearnerCourseAccessTime("learnerId", "courseId");
            
            Assert.That(_sut.LearnerId, Is.EqualTo("learnerId"));
            Assert.That(_sut.CourseId, Is.EqualTo("courseId"));
            Assert.That(_sut.AccessDate, Is.Not.Null);
        }
    }
}