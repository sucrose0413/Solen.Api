using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Dashboard.Services.Queries;

namespace Dashboard.Services.UnitTests.Queries.GetLearningPathsInfo
{
    [TestFixture]
    public class GetLearningPathsInfoServiceTests
    {
        private Mock<IGetLearningPathsInfoRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetLearningPathsInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearningPathsInfoRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetLearningPathsInfoService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPaths_WhenCalled_ReturnLearningPathsList()
        {
            var learningPaths = new List<LearningPathForDashboardDto>();
            _repo.Setup(x => x.GetLearningPaths("organizationId", default))
                .ReturnsAsync(learningPaths);

            var result = _sut.GetLearningPaths(default).Result;

            Assert.That(result, Is.EqualTo(learningPaths));
        }
    }
}