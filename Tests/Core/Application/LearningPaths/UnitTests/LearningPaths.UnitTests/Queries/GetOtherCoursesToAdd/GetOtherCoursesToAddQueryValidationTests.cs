using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetOtherCoursesToAdd
{
    [TestFixture]
    public class GetOtherCoursesToAddQueryValidationTests
    {
        private GetOtherCoursesToAddQueryValidator _sut;
        private GetOtherCoursesToAddQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetOtherCoursesToAddQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _query = new GetOtherCoursesToAddQuery(learningPathId);

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _query);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _query = new GetOtherCoursesToAddQuery("learningPathId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _query);
        }
    }
}