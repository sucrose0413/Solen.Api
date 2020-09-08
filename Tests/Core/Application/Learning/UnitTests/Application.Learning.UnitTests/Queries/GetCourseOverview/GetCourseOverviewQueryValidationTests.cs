using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCourseOverview
{
    [TestFixture]
    public class GetCourseOverviewQueryValidationTests
    {
        private GetCourseOverviewQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetCourseOverviewQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var query = new GetCourseOverviewQuery {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, query);
        }

        [Test]
        public void ValidCourseId_ShouldNotHaveError()
        {
            var query = new GetCourseOverviewQuery {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, query);
        }
    }
}