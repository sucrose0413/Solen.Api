using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.DeleteLearningPath
{
    [TestFixture]
    public class DeleteLearningPathCommandValidationTests
    {
        private DeleteLearningPathCommandValidator _sut;
        private DeleteLearningPathCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new DeleteLearningPathCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }
        
        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new DeleteLearningPathCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }
    }
}