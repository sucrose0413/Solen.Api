using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.UserProfile.Services.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UserProfile.Services.UnitTests.Commands.UpdateCurrentUserInfo
{
    [TestFixture]
    public class UpdateCurrentUserInfoServiceTests
    {
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateCurrentUserInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateCurrentUserInfoService(_userManager.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("userId");
        }

        [Test]
        public void GetCurrentUser_WhenCalled_ReturnTheCurrentUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId"))
                .ReturnsAsync(user);

            var result = _sut.GetCurrentUser(default).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void UpdateCurrentUserName_WhenCalled_UpdateCurrentUserName()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.UpdateCurrentUserName(user.Object, "new user name");

            user.Verify(x => x.UpdateUserName("new user name"));
        }

        [Test]
        public void UpdateCurrentUserRepo_WhenCalled_UpdateCurrentUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateCurrentUserRepo(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }
    }
}