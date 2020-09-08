using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.Services.UnitTests.Commands.UpdateLearningPath
{
    [TestFixture]
    public class UpdateLearningPathServiceTests
    {
        private Mock<IUpdateLearningPathRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateLearningPathRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateLearningPathService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPath_LearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync((LearningPath) null);

            Assert.That(() => _sut.GetLearningPath("learningPathId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetLearningPath_LearningPathDoesExist_ReturnTheCorrectLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync(learningPath);

            var result = _sut.GetLearningPath("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(learningPath));
        }

        [Test]
        public void UpdateName_WhenCalled_UpdateTheLearningPathName()
        {
            var learningPath = new Mock<LearningPath>("name", "organizationId", true);

            _sut.UpdateName(learningPath.Object, "new name");

            learningPath.Verify(x => x.UpdateName("new name"));
        }

        [Test]
        public void UpdateDescription_WhenCalled_UpdateTheLearningPathDescription()
        {
            var learningPath = new Mock<LearningPath>("name", "organizationId", true);

            _sut.UpdateDescription(learningPath.Object, "new description");

            learningPath.Verify(x => x.UpdateDescription("new description"));
        }

        [Test]
        public void UpdateLearningPathRepo_WhenCalled_UpdateLearningPathRepo()
        {
            var learningPath = new LearningPath("name", "organizationId");

            _sut.UpdateLearningPathRepo(learningPath);

            _repo.Verify(x => x.UpdateLearningPath(learningPath));
        }
    }
}