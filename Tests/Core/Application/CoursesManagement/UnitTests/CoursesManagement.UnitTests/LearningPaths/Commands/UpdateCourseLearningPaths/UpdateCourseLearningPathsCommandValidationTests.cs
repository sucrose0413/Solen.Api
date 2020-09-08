using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;

namespace CoursesManagement.UnitTests.LearningPaths.Commands.UpdateCourseLearningPaths
{
    [TestFixture]
    public class UpdateCourseLearningPathsCommandValidationTests
    {
        private UpdateCourseLearningPathsCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateCourseLearningPathsCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CourseIdIsEmptyOrNull_ShouldHaveError(string courseId)
        {
            var command = new UpdateCourseLearningPathsCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }

        [Test]
        public void LearningPathsIdsListIsNull_ShouldHaveError()
        {
            var command = new UpdateCourseLearningPathsCommand();

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathsIds, command);
        }

        [Test]
        public void ValidParameters_ShouldNotHaveError()
        {
            var command = new UpdateCourseLearningPathsCommand
            {
                CourseId = "courseId",
                LearningPathsIds = new List<string> {"learningPathId"}
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathsIds, command);
        }
    }
}