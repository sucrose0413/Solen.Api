using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace LearningPaths.Services.UnitTests.Commands.DeleteLearningPath
{
    [TestFixture]
    public class DeleteLearningPathServiceTests
    {
        private Mock<IDeleteLearningPathRepository> _repo;
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private DeleteLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IDeleteLearningPathRepository>();
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new DeleteLearningPathService(_repo.Object, _userManager.Object, _currentUserAccessor.Object);

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
        public void CheckIfDeletable_LearningPathIsNotDeletable_ThrowNonDeletableLearningPathException()
        {
            var learningPath = new Mock<LearningPath>("name", "organizationId", false);
            learningPath.Setup(x => x.IsDeletable).Returns(false);

            Assert.That(() => _sut.CheckIfDeletable(learningPath.Object),
                Throws.Exception.TypeOf<NonDeletableLearningPathException>());
        }

        [Test]
        public void CheckIfDeletable_LearningPathIsDeletable_ThrowNoException()
        {
            var learningPath = new Mock<LearningPath>("name", "organizationId", false);
            learningPath.Setup(x => x.IsDeletable).Returns(true);

            Assert.That(() => _sut.CheckIfDeletable(learningPath.Object), Throws.Nothing);
        }

        [Test]
        public void GetLearningPathUsers_WhenCalled_GetLearningPathUsers()
        {
            var learners = new List<User>();
            _repo.Setup(x => x.GetLearningPathUsers("learningPathId", default))
                .ReturnsAsync(learners);

            var result = _sut.GetLearningPathUsers("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(learners));
        }

        [Test]
        public void GetGeneralLearningPath_GeneralLearningPathDoesNotExist_ThrowGeneralLearningPathNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPathByName("General", "organizationId", default))
                .ReturnsAsync((LearningPath) null);

            Assert.That(() => _sut.GetGeneralLearningPath(default),
                Throws.Exception.TypeOf<GeneralLearningPathNotFoundException>());
        }

        [Test]
        public void GetGeneralLearningPath_GeneralLearningPathDoesExist_ReturnGeneralLearningPath()
        {
            var generalLearningPath = new LearningPath("General", "organizationId");
            _repo.Setup(x => x.GetLearningPathByName("General", "organizationId", default))
                .ReturnsAsync(generalLearningPath);

            var result = _sut.GetGeneralLearningPath(default).Result;

            Assert.That(result, Is.EqualTo(generalLearningPath));
        }

        [Test]
        public void ChangeUsersLearningPathToGeneral_WhenCalled_ChangeUsersLearningPathToGeneral()
        {
            // Arrange
            var learner1 = new Mock<User>("email", "organizationId");
            var learner2 = new Mock<User>("email", "organizationId");
            var learners = new List<User> {learner1.Object, learner2.Object};
            var generalLearningPath = new LearningPath("General", "organizationId");

            // Act
            _sut.ChangeUsersLearningPathToGeneral(learners, generalLearningPath);

            // Assert
            learner1.Verify(x => x.UpdateLearningPath(generalLearningPath));
            learner2.Verify(x => x.UpdateLearningPath(generalLearningPath));
        }

        [Test]
        public void UpdateUsersRepo_WhenCalled_UpdateUsersRepo()
        {
            var learners = new List<User> {new User("email", "organizationId")};

            _sut.UpdateUsersRepo(learners);

            _userManager.Verify(x => x.UpdateUser(It.IsIn<User>(learners)),
                Times.Exactly(learners.Count));
        }
        
        [Test]
        public void RemoveLearningPathFromRepo_WhenCalled_RemoveLearningPathFromRepo()
        {
            var learningPath = new LearningPath("General", "organizationId");

            _sut.RemoveLearningPathFromRepo(learningPath);

            _repo.Verify(x => x.RemoveLearningPath(learningPath));
        }
    }
}