using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;

namespace CoursesManagement.UnitTests.Lectures.Commands.UpdateLecture
{
    [TestFixture]
    public class UpdateLectureCommandValidationTests
    {
        private UpdateLectureCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateLectureCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new UpdateLectureCommand {LectureId = lectureId};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TitleIsEmptyOrNull_ShouldHaveError(string title)
        {
            var command = new UpdateLectureCommand {Title = title};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void TitleLengthOver100_ShouldHaveError()
        {
            var command = new UpdateLectureCommand {Title = new string('*', 101)};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }


        [Test]
        public void ValidParameters_ShouldNotHaveErrors()
        {
            var command = new UpdateLectureCommand
                {LectureId = "lectureId", Title = "title"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Title, command);
        }
    }
}