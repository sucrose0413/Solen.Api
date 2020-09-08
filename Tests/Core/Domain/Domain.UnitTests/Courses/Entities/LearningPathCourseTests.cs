using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class LearningPathCourseTests
    {
        private LearningPathCourse _sut;

        [Test]
        public void ConstructorWithLearningPathIdCourseIdOrder_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new LearningPathCourse("learningPathId", "courseId", 1);

            Assert.That(_sut.LearningPathId, Is.EqualTo("learningPathId"));
            Assert.That(_sut.CourseId, Is.EqualTo("courseId"));
            Assert.That(_sut.Order, Is.EqualTo(1));
        }
        
        [Test]
        public void UpdateOrder_WhenCalled_UpdateCourseOrderAmongTheLearningPathCourses()
        {
            _sut = new LearningPathCourse("learningPathId", "courseId", 1);

            _sut.UpdateOrder(2);
            
            Assert.That(_sut.Order, Is.EqualTo(2));
        }
    }
}