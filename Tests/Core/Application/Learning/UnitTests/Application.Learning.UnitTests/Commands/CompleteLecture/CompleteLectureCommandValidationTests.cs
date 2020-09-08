using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;

namespace Application.Learning.UnitTests.Commands.CompleteLecture
{
    [TestFixture]
    public class CompleteLectureCommandValidationTests
    {
        private CompleteLectureCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CompleteLectureCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new CompleteLectureCommand {LectureId = lectureId};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }

        [Test]
        public void LectureIdIsValid_ShouldNotHaveError()
        {
            var command = new CompleteLectureCommand {LectureId = "lectureId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
        }
    }
}