using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;

namespace CoursesManagement.UnitTests.Lectures.Commands.UploadMedia
{
    [TestFixture]
    public class UploadMediaCommandValidationTests
    {
        private UploadMediaCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UploadMediaCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new UploadMediaCommand(lectureId, null, null);

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureTypeIsEmptyOrNull_ShouldHaveError(string lectureType)
        {
            var command = new UploadMediaCommand(null, lectureType, null);

            _sut.ShouldHaveValidationErrorFor(x => x.LectureType, command);
        }

        [Test]
        public void ValidData_ShouldNotHaveError()
        {
            var command = new UploadMediaCommand("lectureId", "lectureType", null);

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
        }
    }
}