using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.CreateLearningPath
{
    [TestFixture]
    public class CreateLearningPathCommandValidationTests
    {
        private CreateLearningPathCommandValidator _sut;
        private CreateLearningPathCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NameIsNullOrEmpty_ShouldHaveError(string name)
        {
            _command = new CreateLearningPathCommand {Name = name};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, _command);
        }

        [Test]
        public void NameLengthIsOver50_ShouldHaveError()
        {
            _command = new CreateLearningPathCommand {Name = new string('*', 51)};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, _command);
        }

        [Test]
        public void NameIsValid_ShouldNotHaveError()
        {
            _command = new CreateLearningPathCommand {Name = "learning path name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Name, _command);
        }
    }
}