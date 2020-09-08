using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;

namespace LearningPaths.UnitTests.Commands.UpdateCoursesOrders
{
    [TestFixture]
    public class UpdateCoursesOrdersCommandValidationTests
    {
        private UpdateCoursesOrdersCommandValidator _sut;
        private UpdateCoursesOrdersCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateCoursesOrdersCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new UpdateCoursesOrdersCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new UpdateCoursesOrdersCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void CoursesOrdersListIsNull_ShouldHaveError()
        {
            _command = new UpdateCoursesOrdersCommand {CoursesOrders = null};

            _sut.ShouldHaveValidationErrorFor(x => x.CoursesOrders, _command);
        }

        [Test]
        public void CoursesOrdersListIsEmpty_ShouldHaveError()
        {
            _command = new UpdateCoursesOrdersCommand {CoursesOrders = new List<CourseOrderDto>()};

            _sut.ShouldHaveValidationErrorFor(x => x.CoursesOrders, _command);
        }

        [Test]
        public void CoursesOrdersListIsValid_ShouldNotHaveError()
        {
            _command = new UpdateCoursesOrdersCommand {CoursesOrders = new List<CourseOrderDto> {new CourseOrderDto()}};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CoursesOrders, _command);
        }
    }
}