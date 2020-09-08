using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Learning.Commands;

namespace Application.Learning.UnitTests.Commands.AddLearnerAccessHistory
{
    [TestFixture]
    public class AddLearnerAccessHistoryCommandValidationTests
    {
        private AddLearnerAccessHistoryCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddLearnerAccessHistoryCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureIdIsEmptyOrNull_ShouldHaveError(string lectureId)
        {
            var command = new AddLearnerAccessHistoryCommand {LectureId = lectureId};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, command);
        }

        [Test]
        public void LectureIdIsValid_ShouldNotHaveError()
        {
            var command = new AddLearnerAccessHistoryCommand {LectureId = "lectureId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, command);
        }
    }
}