using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.UpdateLearningPath
{
    [TestFixture]
    public class UpdateLearningPathCommandValidationTests
    {
        private UpdateLearningPathCommandValidator _sut;
        private UpdateLearningPathCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new UpdateLearningPathCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new UpdateLearningPathCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NameIsNullOrEmpty_ShouldHaveError(string name)
        {
            _command = new UpdateLearningPathCommand {Name = name};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, _command);
        }
        
        [Test]
        public void NameLengthIsOver50_ShouldHaveError()
        {
            _command = new UpdateLearningPathCommand {Name = new string('*', 51)};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, _command);
        }

        [Test]
        public void NameIsValid_ShouldNotHaveError()
        {
            _command = new UpdateLearningPathCommand {Name = "learning path name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Name, _command);
        }
        
        [Test]
        public void DescriptionLengthIsOver100_ShouldHaveError()
        {
            _command = new UpdateLearningPathCommand {Description = new string('*', 101)};

            _sut.ShouldHaveValidationErrorFor(x => x.Description, _command);
        }
        
        [Test]
        public void DescriptionIsValid_ShouldNotHaveError()
        {
            _command = new UpdateLearningPathCommand {Description = "description"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Description, _command);
        }
    }
}