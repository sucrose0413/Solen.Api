using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;

namespace CoursesManagement.UnitTests.Courses.Commands.PublishCourse
{
    [TestFixture]
    public class PublishCourseCommandValidationTests
    {
        private PublishCourseCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new PublishCourseCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var command = new PublishCourseCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }
        
        [Test]
        public void ValidCourseId_ShouldNotHaveError()
        {
            var command = new PublishCourseCommand {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
        }
    }
}