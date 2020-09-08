using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPathsList
{
    [TestFixture]
    public class GetLearningPathsQueryHandlerTests
    {
        private GetLearningPathsQueryHandler _sut;
        private Mock<IGetLearningPathsService> _service;
        private GetLearningPathsQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearningPathsService>();
            _sut = new GetLearningPathsQueryHandler(_service.Object);

            _query = new GetLearningPathsQuery();
        }

        [Test]
        public void WhenCalled_ReturnLearningPathLearners()
        {
            var learningPaths = new List<LearningPathDto>();
            _service.Setup(x => x.GetLearningPaths(default))
                .ReturnsAsync(learningPaths);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearningPaths, Is.EqualTo(learningPaths));
        }
    }
}