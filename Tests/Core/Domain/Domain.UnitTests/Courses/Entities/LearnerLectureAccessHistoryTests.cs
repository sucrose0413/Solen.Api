using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class LearnerLectureAccessHistoryTests
    {
        private LearnerLectureAccessHistory _sut;

        [Test]
        public void ConstructorWithLearnerIdLectureId_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new LearnerLectureAccessHistory("learnerId", "lectureId");
            
            Assert.That(_sut.LearnerId, Is.EqualTo("learnerId"));
            Assert.That(_sut.LectureId, Is.EqualTo("lectureId"));
            Assert.That(_sut.AccessDate, Is.Not.Null);
        }
    }
}