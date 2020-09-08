using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Dashboard.Queries;

namespace Dashboard.UnitTests.Queries.GetLearningPathsInfo
{
    [TestFixture]
    public class GetLearningPathsInfoQueryHandlerTests
    {
        private Mock<IGetLearningPathsInfoService> _service;
        private GetLearningPathsInfoQueryHandler _sut;
        private GetLearningPathsInfoQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearningPathsInfoService>();
            _sut = new GetLearningPathsInfoQueryHandler(_service.Object);
            _query = new GetLearningPathsInfoQuery();
        }

        [Test]
        public void WhenCalled_ReturnLearningPathsList()
        {
            var learningPaths = new List<LearningPathForDashboardDto>();
            _service.Setup(x => x.GetLearningPaths(default))
                .ReturnsAsync(learningPaths);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearningPaths, Is.EqualTo(learningPaths));
        }
    }
}