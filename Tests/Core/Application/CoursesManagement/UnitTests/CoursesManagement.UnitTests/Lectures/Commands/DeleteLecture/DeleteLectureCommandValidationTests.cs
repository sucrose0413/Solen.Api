using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;

namespace CoursesManagement.UnitTests.Lectures.Commands.DeleteLecture
{
    [TestFixture]
    public class DeleteLectureCommandValidationTests
    {
        private DeleteLectureCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteLectureCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new DeleteLectureCommand {LectureId = lectureId};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }
        
        [Test]
        public void ValidLectureId_ShouldNotHaveError()
        {
            var command = new DeleteLectureCommand {LectureId = "lectureId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
        }
        
    }
}