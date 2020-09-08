using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.RemoveCourseFromLearningPath
{
    [TestFixture]
    public class RemoveCourseFromLearningPathCommandValidationTests
    {
        private RemoveCourseFromLearningPathCommandValidator _sut;
        private RemoveCourseFromLearningPathCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new RemoveCourseFromLearningPathCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new RemoveCourseFromLearningPathCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }
        
        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new RemoveCourseFromLearningPathCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CourseIdIsNullOrEmpty_ShouldHaveError(string courseId)
        {
            _command = new RemoveCourseFromLearningPathCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, _command);
        }
        
        [Test]
        public void CourseIdIsValid_ShouldNotHaveError()
        {
            _command = new RemoveCourseFromLearningPathCommand {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, _command);
        }
    }
}