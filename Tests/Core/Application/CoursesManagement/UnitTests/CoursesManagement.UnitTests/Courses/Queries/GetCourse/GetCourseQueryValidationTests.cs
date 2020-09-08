using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace CoursesManagement.UnitTests.Courses.Queries.GetCourse
{
    [TestFixture]
    public class GetCourseQueryValidationTests
    {
        private GetCourseQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetCourseQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var query = new GetCourseQuery {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, query);
        }
        
        [Test]
        public void ValidCourseId_ShouldNotHaveError()
        {
            var query = new GetCourseQuery {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, query);
        }
    }
}