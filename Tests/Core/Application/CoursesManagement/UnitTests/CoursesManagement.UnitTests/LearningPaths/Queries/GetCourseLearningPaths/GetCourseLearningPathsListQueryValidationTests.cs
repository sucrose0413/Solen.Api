using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.LearningPaths.Queries;

namespace CoursesManagement.UnitTests.LearningPaths.Queries.GetCourseLearningPaths
{
    [TestFixture]
    public class GetCourseLearningPathsListQueryValidationTests
    {
        private GetCourseLearningPathsListQueryValidator _sut;
        private GetCourseLearningPathsListQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetCourseLearningPathsListQueryValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CourseIdIsEmptyOrNull_ShouldHaveError(string courseId)
        {
            _query = new GetCourseLearningPathsListQuery {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, _query);
        }

        [Test]
        public void ValidCourseId_ShouldNotHaveErrors()
        {
            _query = new GetCourseLearningPathsListQuery {CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, _query);
        }
    }
}