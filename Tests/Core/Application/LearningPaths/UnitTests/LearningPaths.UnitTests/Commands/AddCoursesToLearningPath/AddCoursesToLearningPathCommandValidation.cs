using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.AddCoursesToLearningPath
{
    [TestFixture]
    public class AddCoursesToLearningPathCommandValidation
    {
        private AddCoursesToLearningPathCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddCoursesToLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsEmptyOrNull_ShouldHaveError(string learningPathId)
        {
            var command = new AddCoursesToLearningPathCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, command);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            var command = new AddCoursesToLearningPathCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, command);
        }

        [Test]
        [TestCase(null)]
        public void CoursesIdsListIsNull_ShouldHaveError(List<string> coursesIds)
        {
            var command = new AddCoursesToLearningPathCommand {CoursesIds = coursesIds};

            _sut.ShouldHaveValidationErrorFor(x => x.CoursesIds, command);
        }

        [Test]
        public void CoursesIdsListIsValid_ShouldNotHaveError()
        {
            var command = new AddCoursesToLearningPathCommand {CoursesIds = new List<string> {"courseId"}};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CoursesIds, command);
        }
    }
}