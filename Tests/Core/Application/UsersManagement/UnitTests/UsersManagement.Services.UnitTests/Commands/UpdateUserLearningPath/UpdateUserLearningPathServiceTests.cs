using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.Services.UnitTests.Commands.UpdateUserLearningPath
{
    [TestFixture]
    public class UpdateUserLearningPathServiceTests
    {
        private Mock<IUpdateUserLearningPathRepository> _repo;
        private Mock<IUserManager> _userManager;
        private UpdateUserLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateUserLearningPathRepository>();
            _userManager = new Mock<IUserManager>();
            _sut = new UpdateUserLearningPathService(_repo.Object, _userManager.Object);
        }

        [Test]
        public void GetUserFromRepo_TheUserDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserFromRepo("userId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUserFromRepo_TheUserDoesExist_ReturnUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(user);

            var result = _sut.GetUserFromRepo("userId", default).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void GetLearningPath_TheLearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPath("learningPathId", default))
                .ReturnsAsync((LearningPath) null);

            Assert.That(() => _sut.GetLearningPath("learningPathId", default),
                Throws.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void GetLearningPath_TheLearningPathDoesExist_ReturnTheLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _repo.Setup(x => x.GetLearningPath("learningPathId", default))
                .ReturnsAsync(learningPath);

            var result = _sut.GetLearningPath("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(learningPath));
        }
        
        [Test]
        public void UpdateUserLearningPath_WhenCalled_UpdateTheUserLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            var user = new Mock<User>("email", "organizationId");

            _sut.UpdateUserLearningPath(user.Object, learningPath);

            user.Verify(x => x.UpdateLearningPath(learningPath));
        }
        
        [Test]
        public void UpdateUser_WhenCalled_UpdateTheUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUserRepo(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }
    }
}