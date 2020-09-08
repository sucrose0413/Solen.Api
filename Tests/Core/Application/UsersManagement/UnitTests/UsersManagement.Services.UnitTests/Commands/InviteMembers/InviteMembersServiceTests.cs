using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;

namespace UsersManagement.Services.UnitTests.Commands.InviteMembers
{
    [TestFixture]
    public class InviteMembersServiceTests
    {
        private Mock<IUserManager> _userManager;
        private Mock<IInviteMembersRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private Mock<IRandomTokenGenerator> _tokenGenerator;
        private InviteMembersService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _repo = new Mock<IInviteMembersRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _tokenGenerator = new Mock<IRandomTokenGenerator>();
            _sut = new InviteMembersService(_userManager.Object, _repo.Object, _currentUserAccessor.Object,
                _tokenGenerator.Object);

            _currentUserAccessor.Setup(x => x.Username).Returns("current user name");
            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void CheckEmailExistence_TheEmailAlreadyExists_ThrowEmailAlreadyRegisteredException()
        {
            _userManager.Setup(x => x.DoesEmailExistAsync("email")).ReturnsAsync(true);

            Assert.That(() => _sut.CheckEmailExistence("email"),
                Throws.TypeOf<EmailAlreadyRegisteredException>());
        }

        [Test]
        public void CheckEmailExistence_TheEmailDoesNotExist_ThrowNoException()
        {
            _userManager.Setup(x => x.DoesEmailExistAsync("email")).ReturnsAsync(false);

            Assert.That(() => _sut.CheckEmailExistence("email"), Throws.Nothing);
        }

        [Test]
        public void GetLearningPathFromRepo_TheLearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync((LearningPath) null);

            Assert.That(() => _sut.GetLearningPathFromRepo("learningPathId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetLearningPathFromRepo_TheLearningPathDoesExist_ReturnTheLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _repo.Setup(x => x.GetLearningPath("learningPathId", "organizationId", default))
                .ReturnsAsync(learningPath);

            var result = _sut.GetLearningPathFromRepo("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(learningPath));
        }

        [Test]
        public void CreateUsers_WhenCalled_ReturnCreatedUsersListWithCorrectEmails()
        {
            var emails = new List<string> {"email1", "email2"};

            var result = _sut.CreateUsers(emails);

            Assert.That(result.Select(x => x.Email), Is.EqualTo(emails));
        }

        [Test]
        public void CreateUsers_WhenCalled_ReturnCreatedUsersListWithCorrectOrganizationId()
        {
            var emails = new List<string> {"email1", "email2"};

            var result = _sut.CreateUsers(emails);

            Assert.That(result.Select(x => x.OrganizationId), Is.All.EqualTo("organizationId"));
        }

        [Test]
        public void SetTheInviteeToUser_WhenCalled_SetTheNameofTheInvitee()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.SetTheInviteeToUser(user.Object);

            user.Verify(x => x.SetInvitedBy("current user name"));
        }

        [Test]
        public void SetTheInvitationTokenToUser_WhenCalled_SetTheInvitationTokenToUser()
        {
            _tokenGenerator.Setup(x => x.CreateToken(100)).Returns("token");
            var user = new Mock<User>("email", "organizationId");

            _sut.SetTheInvitationTokenToUser(user.Object);

            user.Verify(x => x.SetInvitationToken("token"));
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
        public void AddLearnerRoleToUser_WhenCalled_AddLearnerRoleToTheUser()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.AddLearnerRoleToUser(user.Object);

            user.Verify(x => x.AddRoleId(UserRoles.Learner));
        }

        [Test]
        public void AddUserToRepo_WhenCalled_AddTheUserToTheRepo()
        {
            var user = new User("email", "organizationId");

            _sut.AddUserToRepo(user);

            _userManager.Verify(x => x.CreateAsync(user));
        }
    }
}