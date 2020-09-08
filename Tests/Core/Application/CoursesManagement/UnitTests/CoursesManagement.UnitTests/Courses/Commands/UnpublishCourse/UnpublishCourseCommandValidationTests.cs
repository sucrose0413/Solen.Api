using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;

namespace CoursesManagement.UnitTests.Courses.Commands.UnpublishCourse
{
    [TestFixture]
    public class UnpublishCourseCommandValidationTests
    {
        private UnpublishCourseCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UnpublishCourseCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var command = new UnpublishCourseCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }
        
        [Test]
        public void ValidCourseId_ShouldNotHaveError()
        {
            var command = new UnpublishCourseCommand {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
        }
    }
}