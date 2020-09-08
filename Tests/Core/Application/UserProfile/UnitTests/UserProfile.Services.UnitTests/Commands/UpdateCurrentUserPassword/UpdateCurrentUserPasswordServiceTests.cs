using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.UserProfile.Services.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UserProfile.Services.UnitTests.Commands.UpdateCurrentUserPassword
{
    [TestFixture]
    public class UpdateCurrentUserPasswordServiceTests
    {
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateCurrentUserPasswordService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateCurrentUserPasswordService(_userManager.Object, _currentUserAccessor.Object);

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
        public void UpdateCurrentUserPassword_WhenCalled_UpdateCurrentUserPassword()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateCurrentUserPassword(user, "new user password");

            _userManager.Verify(x => x.UpdatePassword(user, "new user password"));
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