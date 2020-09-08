using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace LearningPaths.Services.UnitTests.Queries.GetLearningPaths
{
    [TestFixture]
    public class GetLearningPathsServiceTests
    {
        private Mock<IGetLearningPathsRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetLearningPathsService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearningPathsRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetLearningPathsService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPaths_WhenCalled_ReturnTheLearningPathsList()
        {
            var learningPaths = new List<LearningPathDto>();
            _repo.Setup(x => x.GetLearningPaths("organizationId", default))
                .ReturnsAsync(learningPaths);

            var result = _sut.GetLearningPaths(default).Result;

            Assert.That(result, Is.EqualTo(learningPaths));
        }
    }
}