using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.UserProfile.Services.Queries;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace UserProfile.Services.UnitTests.Queries.GetCurrentUserInfo
{
    [TestFixture]
    public class GetCurrentUserInfoServiceTests
    {
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCurrentUserInfoService _sut;

        private User _currentUser;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCurrentUserInfoService(_userManager.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("userId");

            _currentUser = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(_currentUser);
        }

        [Test]
        public void GetCurrentUserInfo_WhenCalled_ReturnTheCurrentUserName()
        {
            _currentUser.UpdateUserName("userName");

            var result = _sut.GetCurrentUserInfo(default).Result;

            Assert.That(result.UserName, Is.EqualTo(_currentUser.UserName));
        }

        [Test]
        public void GetCurrentUserInfo_WhenCalled_ReturnTheCurrentUserEmail()
        {
            var result = _sut.GetCurrentUserInfo(default).Result;

            Assert.That(result.Email, Is.EqualTo(_currentUser.Email));
        }

        [Test]
        public void GetCurrentUserInfo_WhenCalled_ReturnTheCurrentUserLearningPath()
        {
            _currentUser.UpdateLearningPath(new LearningPath("name", "organizationId"));

            var result = _sut.GetCurrentUserInfo(default).Result;

            Assert.That(result.LearningPath, Is.EqualTo(_currentUser.LearningPath.Name));
        }

        [Test]
        public void GetCurrentUserInfo_WhenCalled_ReturnTheCurrentUserRoles()
        {
            // Arrange
            var currentUser = new Mock<User>("email", "organizationId");
            var userRoles = new List<UserRole>
            {
                new UserRole("userId", new Role("roleId1", "role 1", "description")),
                new UserRole("userId", new Role("roleId2", "role 2", "description"))
            };
            currentUser.Setup(x => x.UserRoles).Returns(userRoles);
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(currentUser.Object);

            // Act
            var result = _sut.GetCurrentUserInfo(default).Result;

            // Assert
            Assert.That(result.Roles, Is.EqualTo("role 1,role 2"));
        }
    }
}