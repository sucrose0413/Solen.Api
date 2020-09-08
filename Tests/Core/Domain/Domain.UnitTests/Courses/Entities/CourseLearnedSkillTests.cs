using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class CourseLearnedSkillTests
    {
        private CourseLearnedSkill _sut;
        
        [Test]
        public void ConstructorWithCourseIdName_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new CourseLearnedSkill("courseId", "name");

            Assert.That(_sut.CourseId, Is.EqualTo("courseId"));
            Assert.That(_sut.Name, Is.EqualTo("name"));
        }
    }
}