using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class LectureTests
    {
        private Lecture _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new VideoLecture("title", "moduleId", 1);
        }
        
        [Test]
        public void UpdateTitle_WhenCalled_UpdateLectureTitle()
        {
            _sut.UpdateTitle("new title");

            Assert.That(_sut.Title, Is.EqualTo("new title"));
        }
        
        [Test]
        public void UpdateDuration_WhenCalled_UpdateLectureDuration()
        {
            _sut.UpdateDuration(10);

            Assert.That(_sut.Duration, Is.EqualTo(10));
        }
        
        [Test]
        public void UpdateOrder_WhenCalled_UpdateLectureOrder()
        {
            _sut.UpdateOrder(2);

            Assert.That(_sut.Order, Is.EqualTo(2));
        }
    }
}