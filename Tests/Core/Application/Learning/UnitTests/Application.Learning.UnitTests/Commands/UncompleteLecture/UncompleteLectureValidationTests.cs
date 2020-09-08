using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;

namespace Application.Learning.UnitTests.Commands.UncompleteLecture
{
    [TestFixture]
    public class UncompleteLectureValidationTests
    {
        private UncompleteLectureCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UncompleteLectureCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new UncompleteLectureCommand(lectureId);

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }

        [Test]
        public void LectureIdIsValid_ShouldNotHaveError()
        {
            var command = new UncompleteLectureCommand("lectureId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
        }
    }
}