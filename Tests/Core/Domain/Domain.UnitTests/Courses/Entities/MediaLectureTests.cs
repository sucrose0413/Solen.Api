using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class MediaLectureTests
    {
        private MediaLecture _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new VideoLecture("title", "moduleId", 1);
        }
        
        [Test]
        public void SetUrl_WhenCalled_SetTheMediaUrl()
        {
            _sut.SetUrl("url");

            Assert.That(_sut.Url, Is.EqualTo("url"));
        }
    }
}