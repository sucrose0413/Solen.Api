using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPath
{
    [TestFixture]
    public class GetLearningPathQueryValidationTests
    {
        private GetLearningPathQueryValidator _sut;
        private GetLearningPathQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetLearningPathQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _query = new GetLearningPathQuery(learningPathId);

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _query);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _query = new GetLearningPathQuery("learningPathId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _query);
        }
    }
}