using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using VideoLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.VideoLecture;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class VideoLectureTests
    {
        private Solen.Core.Domain.Courses.Entities.VideoLecture _sut;

        [Test]
        public void ConstructorWithTitleModuleIdOrder_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new Solen.Core.Domain.Courses.Entities.VideoLecture("title", "moduleId", 1);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Title, Is.EqualTo("title"));
            Assert.That(_sut.ModuleId, Is.EqualTo("moduleId"));
            Assert.That(_sut.Order, Is.EqualTo(1));
            Assert.That(_sut.LectureType, Is.TypeOf<VideoLecture>());
        }

        [Test]
        public void ConstructorWithTitleModuleOrder_WhenCalled_SetPropertiesCorrectly()
        {
            var module = new Module("module", "courseId", 1);
            _sut = new Solen.Core.Domain.Courses.Entities.VideoLecture("title", module, 1);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Title, Is.EqualTo("title"));
            Assert.That(_sut.ModuleId, Is.EqualTo(module.Id));
            Assert.That(_sut.Module, Is.EqualTo(module));
            Assert.That(_sut.Order, Is.EqualTo(1));
            Assert.That(_sut.LectureType, Is.TypeOf<VideoLecture>());
        }
    }
}