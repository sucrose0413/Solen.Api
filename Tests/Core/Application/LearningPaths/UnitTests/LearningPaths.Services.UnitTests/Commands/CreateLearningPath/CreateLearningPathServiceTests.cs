using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.Services.UnitTests.Commands.CreateLearningPath
{
    [TestFixture]
    public class CreateLearningPathServiceTests
    {
        private Mock<ICreateLearningPathRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CreateLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICreateLearningPathRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CreateLearningPathService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void CreateLearningPath_WhenCalled_CreateNewLearningPath()
        {
            var result = _sut.CreateLearningPath("name");

            Assert.That(result.Name, Is.EqualTo("name"));
            Assert.That(result.OrganizationId, Is.EqualTo("organizationId"));
        }

        [Test]
        public void AddLearningPathToRepo_WhenCalled_AddLearningPathToRepo()
        {
            var learningPath = new LearningPath("name", "organizationId");

            _sut.AddLearningPathToRepo(learningPath, default).Wait();

            _repo.Verify(x => x.AddLearningPath(learningPath, default));
        }
    }
}