using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPathLearners
{
    [TestFixture]
    public class GetLearningPathLearnersQueryValidationTests
    {
        private GetLearningPathLearnersQueryValidator _sut;
        private GetLearningPathLearnersQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetLearningPathLearnersQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _query = new GetLearningPathLearnersQuery(learningPathId);

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _query);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _query = new GetLearningPathLearnersQuery("learningPathId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _query);
        }
    }
}