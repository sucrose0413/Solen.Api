using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCourseContent
{
    [TestFixture]
    public class GetCourseContentQueryValidationTests
    {
        private GetCourseContentQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetCourseContentQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var query = new GetCourseContentQuery {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, query);
        }
        
        [Test]
        public void ValidCourseId_ShouldNotHaveError()
        {
            var query = new GetCourseContentQuery {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, query);
        }
    }
}