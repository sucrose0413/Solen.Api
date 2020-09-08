using System;
using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class ModuleTests
    {
        private Module _sut;

        [Test]
        public void ConstructorWithNameCourseIdOrder_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new Module("name", "courseId", 1);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Name, Is.EqualTo("name"));
            Assert.That(_sut.CourseId, Is.EqualTo("courseId"));
            Assert.That(_sut.Order, Is.EqualTo(1));
        }
        
        [Test]
        public void ConstructorWithNameCourseOrder_WhenCalled_SetPropertiesCorrectly()
        {
            var course = new Course("title", "creatorId", DateTime.Now);
            _sut = new Module("name", course, 1);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Name, Is.EqualTo("name"));
            Assert.That(_sut.Order, Is.EqualTo(1));
            Assert.That(_sut.CourseId, Is.EqualTo(course.Id));
            Assert.That(_sut.Course, Is.EqualTo(course));
        }
        
        [Test]
        public void UpdateName_WhenCalled_UpdateModuleName()
        {
            _sut = new Module("name", "courseId", 1);

            _sut.UpdateName("new name");

            Assert.That(_sut.Name, Is.EqualTo("new name"));
        }
        
        [Test]
        public void UpdateOrder_WhenCalled_UpdateModuleOrder()
        {
            _sut = new Module("name", "courseId", 1);

            _sut.UpdateOrder(2);

            Assert.That(_sut.Order, Is.EqualTo(2));
        }
        
    }
}