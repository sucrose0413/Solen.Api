using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using VideoLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.VideoLecture;

namespace CoursesManagement.Factories.UnitTests.LectureCreation
{
    [TestFixture]
    public class VideoCreatorTests
    {
        private CreateLectureCommand _command;
        private VideoCreator _sut;

        [SetUp]
        public void SetUp()
        {
            _command = new CreateLectureCommand {Title = "title", ModuleId = "moduleId", Order = 1};
            _sut = new VideoCreator();
        }

        [Test]
        public void Create_WhenCalled_CreateVideoLecture()
        {
            var result = _sut.Create(_command);

            Assert.That(result, Is.TypeOf<Solen.Core.Domain.Courses.Entities.VideoLecture>());
        }

        [Test]
        public void Create_WhenCalled_CreateVideoWithCorrectData()
        {
            var result = _sut.Create(_command);

            Assert.That(result.Title, Is.EqualTo(_command.Title));
            Assert.That(result.ModuleId, Is.EqualTo(_command.ModuleId));
            Assert.That(result.Order, Is.EqualTo(_command.Order));
        }

        [Test]
        public void LectureType_WhenCalled_CreateVideoType()
        {
            var result = _sut.LectureType;

            Assert.That(result, Is.TypeOf<VideoLecture>());
        }
    }
}