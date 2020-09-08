using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPathLearners
{
    [TestFixture]
    public class GetLearningPathLearnersQueryHandlerTests
    {
        private GetLearningPathLearnersQueryHandler _sut;
        private Mock<IGetLearningPathLearnersService> _service;
        private GetLearningPathLearnersQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearningPathLearnersService>();
            _sut = new GetLearningPathLearnersQueryHandler(_service.Object);

            _query = new GetLearningPathLearnersQuery("learnerId");
        }

        [Test]
        public void WhenCalled_ReturnLearningPathLearners()
        {
            var learningPathLearners = new List<LearnerForLearningPathDto>();
            _service.Setup(x => x.GetLearningPathLearners(_query.LearningPathId, default))
                .ReturnsAsync(learningPathLearners);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Learners, Is.EqualTo(learningPathLearners));
        }
    }
}