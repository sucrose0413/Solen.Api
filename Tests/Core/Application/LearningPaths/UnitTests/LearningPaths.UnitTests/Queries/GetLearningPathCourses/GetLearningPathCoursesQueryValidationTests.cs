using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPathCourses
{
    [TestFixture]
    public class GetLearningPathCoursesQueryValidationTests
    {
        private GetLearningPathCoursesQueryValidator _sut;
        private GetLearningPathCoursesQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetLearningPathCoursesQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _query = new GetLearningPathCoursesQuery(learningPathId);

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _query);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _query = new GetLearningPathCoursesQuery("learningPathId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _query);
        }
    }
}