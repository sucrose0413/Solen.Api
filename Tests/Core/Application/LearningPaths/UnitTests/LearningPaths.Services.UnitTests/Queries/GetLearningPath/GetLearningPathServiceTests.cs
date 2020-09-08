using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace LearningPaths.Services.UnitTests.Queries.GetLearningPath
{
    [TestFixture]
    public class GetLearningPathServiceTests
    {
        private Mock<IGetLearningPathRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearningPathRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetLearningPathService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPath_LearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync((LearningPathDto) null);


            Assert.That(() => _sut.GetLearningPath("learningPathId", default),
                Throws.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void GetLearningPath_LearningPathDoesExist_ReturnTheLearningPath()
        {
            var learningPath = new LearningPathDto();
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync(learningPath);

            var result = _sut.GetLearningPath("learningPathId", default).Result;
            
            Assert.That(result, Is.EqualTo(learningPath));
        }
    }
}