using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;

namespace CoursesManagement.UnitTests.Courses.Commands.CreateCourse
{
    [TestFixture]
    public class CreateCourseCommandValidationTests
    {
        private CreateCourseCommandValidator _sut;
        private CreateCourseCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateCourseCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NullOrEmptyTitle_ShouldHaveError(string title)
        {
            _command = new CreateCourseCommand {Title = title};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, _command);
        }

        [Test]
        public void TitleLengthIsOver60_ShouldHaveError()
        {
            _command = new CreateCourseCommand {Title = new string('*', 61)};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, _command);
        }

        [Test]
        public void TitleIsValid_ShouldNotHaveError()
        {
            _command = new CreateCourseCommand {Title = "course name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Title, _command);
        }
    }
}