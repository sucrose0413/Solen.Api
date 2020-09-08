using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearnerProgress
{
    [TestFixture]
    public class GetLearnerProgressQueryValidationTests
    {
        private GetLearnerProgressQueryValidator _sut;
        private GetLearnerProgressQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetLearnerProgressQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearnerIdIsNullOrEmpty_ShouldHaveError(string learnerId)
        {
            _query = new GetLearnerProgressQuery(learnerId);

            _sut.ShouldHaveValidationErrorFor(x => x.LearnerId, _query);
        }

        [Test]
        public void LearnerIdIsValid_ShouldNotHaveError()
        {
            _query = new GetLearnerProgressQuery("learnerId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearnerId, _query);
        }
    }
}