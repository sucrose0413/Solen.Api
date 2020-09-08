using System;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Auth.Services.UnitTests.Queries._Common
{
    [TestFixture]
    public class CommonServiceTests
    {
        private CommonService _sut;
        private Mock<IUserManager> _userManager;
        private Mock<IDateTime> _dateTime;
        private Mock<ISecurityConfig> _securityConfig;
        private Mock<ICommonRepository> _repo;


        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _dateTime = new Mock<IDateTime>();
            _securityConfig = new Mock<ISecurityConfig>();
            _repo = new Mock<ICommonRepository>();

            _sut = new CommonService(_repo.Object, _userManager.Object, _dateTime.Object, _securityConfig.Object);
        }

        [Test]
        public void CheckIfUserIsBlockedOrInactive_UserIsInactive_ThrowLockedException()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.IsActiveUser(user))
                .Returns(false);

            Assert.That(() => _sut.CheckIfUserIsBlockedOrInactive(user),
                Throws.Exception.TypeOf<LockedException>());
        }

        [Test]
        public void CheckIfUserIsBlockedOrInactive_UserIsActive_NotThrowException()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.IsActiveUser(user))
                .Returns(true);

            Assert.That(() => _sut.CheckIfUserIsBlockedOrInactive(user), Throws.Nothing);
        }

        [Test]
        public void CreateUserToken_WhenCalled_CreateUserToken()
        {
            var user = new User("email", "organizationId");
            var claims = new List<Claim>();
            _userManager.Setup(x => x.CreateUserClaims(user)).Returns(claims);
            var userToken = "token";
            _userManager.Setup(x => x.CreateUserToken(claims)).Returns(userToken);

            var result = _sut.CreateUserToken(user);

            Assert.That(result, Is.EqualTo(userToken));
        }

        [Test]
        public void CreateLoggedUser_WhenCalled_CreateLoggedUser()
        {
            var learningPath = new LearningPath("learning path", "organizationId");
            var user = new User("email", "organizationId");
            user.UpdateLearningPath(learningPath);

            var result = _sut.CreateLoggedUser(user);

            Assert.That(result.Id, Is.EqualTo(user.Id));
            Assert.That(result.UserName, Is.EqualTo(user.UserName));
            Assert.That(result.LearningPath, Is.EqualTo(user.LearningPath.Name));
        }

        [Test]
        public void CreateNewRefreshToken_WhenCalled_CreateRefreshTokenForTheUser()
        {
            var user = new User("email", "organizationId");

            var result = _sut.CreateNewRefreshToken(user);

            Assert.That(result.UserId, Is.EqualTo(user.Id));
        }

        [Test]
        public void CreateNewRefreshToken_ExpiryTimeIsEqualToZero_CreateRefreshWithNoExpiryTime()
        {
            var expiryTimeInDays = 0;
            _securityConfig.Setup(x => x.GetRefreshTokenExpiryTimeInDays()).Returns(expiryTimeInDays);
            var user = new User("email", "organizationId");

            var result = _sut.CreateNewRefreshToken(user);

            Assert.That(result.ExpiryTime, Is.Null);
        }

        [Test]
        public void CreateNewRefreshToken_ExpiryTimeIsNotEqualToZero_CreateRefreshTokenWithTheCorrectExpiryTime()
        {
            // Arrange
            var expiryTimeInDays = 7;
            _securityConfig.Setup(x => x.GetRefreshTokenExpiryTimeInDays()).Returns(expiryTimeInDays);
            var now = DateTime.UtcNow;
            _dateTime.Setup(x => x.UtcNow).Returns(now);
            var user = new User("email", "organizationId");

            // Act
            var result = _sut.CreateNewRefreshToken(user);

            // Assert
            Assert.That(result.ExpiryTime, Is.EqualTo(now.AddDays(expiryTimeInDays)));
        }

        [Test]
        public void RemoveAnyUserRefreshToken_WhenCalled_RemoveAnyUserRefreshTokenFromRepo()
        {
            var user = new User("email", "organizationId");

            _sut.RemoveAnyUserRefreshToken(user, default);

            _repo.Verify(x => x.RemoveAnyUserRefreshToken(user.Id, default));
        }

        [Test]
        public void AddNewRefreshTokenToRepo_WhenCalled_AddNewRefreshTokenToRepo()
        {
            var user = new User("userId", "organizationId");
            var refreshToken = new RefreshToken(user, null);

            _sut.AddNewRefreshTokenToRepo(refreshToken);

            _repo.Verify(x => x.AddRefreshToken(refreshToken));
        }
    }
}