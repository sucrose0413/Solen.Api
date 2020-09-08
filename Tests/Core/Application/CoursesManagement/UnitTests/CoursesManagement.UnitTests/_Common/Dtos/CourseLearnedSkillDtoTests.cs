using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;

namespace CoursesManagement.UnitTests._Common.Dtos
{
    [TestFixture]
    public class CourseLearnedSkillDtoTests
    {
        private CourseLearnedSkillDto _sut;

        [Test]
        public void ConstructorWithIdAndName_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new CourseLearnedSkillDto(1, "name");

            Assert.That(_sut.Id, Is.EqualTo(1));
            Assert.That(_sut.Name, Is.EqualTo("name"));
        }
    }
}