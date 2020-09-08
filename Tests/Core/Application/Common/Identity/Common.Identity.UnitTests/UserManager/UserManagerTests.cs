using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Common.Identity.UnitTests
{
    [TestFixture]
    public class UserManagerTests
    {
        private UserManager _sut;
        private Mock<IUserManagerRepo> _repo;
        private Mock<IPasswordHashGenerator> _passwordHashGenerator;
        private Mock<IJwtGenerator> _jwtGenerator;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUserManagerRepo>();
            _passwordHashGenerator = new Mock<IPasswordHashGenerator>();
            _jwtGenerator = new Mock<IJwtGenerator>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new UserManager(_repo.Object, _currentUserAccessor.Object, _passwordHashGenerator.Object,
                _jwtGenerator.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void CreateAsync_WhenCalled_AddUserToRepo()
        {
            var user = new User("email", "organizationId");

            _sut.CreateAsync(user).Wait();

            _repo.Verify(x => x.AddUser(user));
        }

        [Test]
        public void CreateUserToken_WhenCalled_CreateUserToken()
        {
            var claims = new List<Claim>();
            var token = "token";
            _jwtGenerator.Setup(x => x.GenerateToken(claims)).Returns(token);

            var result = _sut.CreateUserToken(claims);

            Assert.That(result, Is.EqualTo(token));
        }

        [Test]
        public void FindByIdAsync_WhenCalled_ReturnTheCorrectUser()
        {
            var user = new User("email", "organizationId");
            _repo.Setup(x => x.FindUserByIdAsync(user.Id, "organizationId"))
                .ReturnsAsync(user);

            var result = _sut.FindByIdAsync(user.Id).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void ValidateUserInscription_WhenCalled_ChangeUserStatusToActive()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.ValidateUserInscription(user.Object);

            user.Verify(x => x.ChangeUserStatus(new ActiveStatus()));
        }

        [Test]
        public void ValidateUserInscription_WhenCalled_InitInvitationToken()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.ValidateUserInscription(user.Object);

            user.Verify(x => x.InitInvitationToken());
        }

        [Test]
        public void UpdatePassword_WhenCalled_UpdateUserPassword()
        {
            // Arrange
            byte[] passwordHash = null;
            byte[] passwordSalt = null;
            var password = "password";
            _passwordHashGenerator.Setup(x => x.CreatePasswordHash(password, out passwordHash, out passwordSalt));
            var user = new Mock<User>("email", "organizationId");

            // Act
            _sut.UpdatePassword(user.Object, password);

            // Assert
            user.Verify(x => x.UpdatePassword(passwordHash, passwordSalt));
        }

        [Test]
        public void UpdateUser_WhenCalled_UpdateUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUser(user);

            _repo.Verify(x => x.UpdateUser(user));
        }

        [Test]
        public void DoesPasswordTokenExist_PasswordTokenDoesNotExist_ReturnFalse()
        {
            _repo.Setup(x => x.DoesPasswordTokenExist("token")).ReturnsAsync(false);

            var result = _sut.DoesPasswordTokenExist("token").Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void DoesPasswordTokenExist_PasswordTokenDoesExist_ReturnTrue()
        {
            _repo.Setup(x => x.DoesPasswordTokenExist("token")).ReturnsAsync(true);

            var result = _sut.DoesPasswordTokenExist("token").Result;

            Assert.That(result, Is.True);
        }

        [Test]
        public void FindByEmailAsync_WhenCalled_ReturnTheCorrectUser()
        {
            var user = new User("email", "organizationId");
            _repo.Setup(x => x.FindUserByEmailAsync("email"))
                .ReturnsAsync(user);

            var result = _sut.FindByEmailAsync(user.Email).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void DoesEmailExistAsync_EmailDoesNotExist_ReturnFalse()
        {
            _repo.Setup(x => x.DoesEmailExistAsync("email")).ReturnsAsync(false);

            var result = _sut.DoesEmailExistAsync("email").Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void DoesEmailExistAsync_EmailDoestExist_ReturnTrue()
        {
            _repo.Setup(x => x.DoesEmailExistAsync("email")).ReturnsAsync(true);

            var result = _sut.DoesEmailExistAsync("email").Result;

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetOrganizationUsersAsync_WhenCalled_ReturnOrganizationUsersList()
        {
            var users = new List<User>();
            _repo.Setup(x => x.GetOrganizationUsersAsync("organizationId"))
                .ReturnsAsync(users);

            var result = _sut.GetOrganizationUsersAsync().Result;

            Assert.That(result, Is.EqualTo(users));
        }

        [Test]
        public void FindByInvitationTokenAsync_WhenCalled_ReturnTheCorrectUser()
        {
            var user = new User("email", "organizationId");
            _repo.Setup(x => x.FindUserByInvitationTokenAsync("invitationToken"))
                .ReturnsAsync(user);

            var result = _sut.FindByInvitationTokenAsync("invitationToken").Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void FindByPasswordTokenAsync_WhenCalled_ReturnTheCorrectUser()
        {
            var user = new User("email", "organizationId");
            _repo.Setup(x => x.FindUserByPasswordTokenAsync("passwordToken"))
                .ReturnsAsync(user);

            var result = _sut.FindByPasswordTokenAsync("passwordToken").Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void CheckPassword_UserIsNull_ReturnFalse()
        {
            User user = null;

            var result = _sut.CheckPassword(user, "password");

            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckPassword_BadPassword_ReturnFalse()
        {
            var user = new User("email", "organization");
            _passwordHashGenerator
                .Setup(x => x.VerifyPasswordHash("wrongPassword", user.PasswordHash, user.PasswordSalt)).Returns(false);

            var result = _sut.CheckPassword(user, "wrongPassword");

            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckPassword_ValidPassword_ReturnTrue()
        {
            var user = new User("email", "organizationId");
            _passwordHashGenerator
                .Setup(x => x.VerifyPasswordHash("password", user.PasswordHash, user.PasswordSalt)).Returns(true);

            var result = _sut.CheckPassword(user, "password");

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsActiveUser_UserIsNull_ReturnFalse()
        {
            User user = null;

            var result = _sut.IsActiveUser(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsActiveUser_UserIsInactive_ReturnFalse()
        {
            var user = new User("email", "organizationId");

            var result = _sut.IsActiveUser(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsActiveUser_UserIsActive_ReturnTrue()
        {
            var user = new User("email", "organizationId");
            user.ChangeUserStatus(new ActiveStatus());

            var result = _sut.IsActiveUser(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetRolesAsync_WhenCalled_ReturnUserRoles()
        {
            var roles = new List<Role>();
            var user = new User("email", "organizationId");
            _repo.Setup(x => x.GetUserRoles(user.Id)).ReturnsAsync(roles);

            var result = _sut.GetRolesAsync(user).Result;

            Assert.That(result, Is.EqualTo(roles));
        }
    }
}