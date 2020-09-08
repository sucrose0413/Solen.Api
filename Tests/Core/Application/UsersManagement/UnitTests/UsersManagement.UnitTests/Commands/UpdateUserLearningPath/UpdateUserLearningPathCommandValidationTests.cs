using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Commands;

namespace UsersManagement.UnitTests.Commands.UpdateUserLearningPath
{
    [TestFixture]
    public class UpdateUserLearningPathCommandValidationTests
    {
        private UpdateUserLearningPathCommandValidator _sut;
        private UpdateUserLearningPathCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateUserLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserIdIsNullOrEmpty_ShouldHaveError(string userId)
        {
            _command = new UpdateUserLearningPathCommand {UserId = userId};

            _sut.ShouldHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        public void UserIdIsValid_ShouldNotHaveError()
        {
            _command = new UpdateUserLearningPathCommand {UserId = "userId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new UpdateUserLearningPathCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new UpdateUserLearningPathCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }
    }
}