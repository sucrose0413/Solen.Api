using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace UsersManagement.Services.UnitTests.Commands.BlockUser
{
    [TestFixture]
    public class BlockUserServiceTests
    {
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private BlockUserService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new BlockUserService(_userManager.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("userId");
        }

        [Test]
        public void CheckIfTheUserToBlockIsTheCurrentUser_SameUser_ThrowSameUserBlockingException()
        {
            Assert.That(() => _sut.CheckIfTheUserToBlockIsTheCurrentUser("userId"),
                Throws.TypeOf<SameUserBlockingException>());
        }

        [Test]
        public void CheckIfTheUserToBlockIsTheCurrentUser_NotTheSameUser_ThrowNoException()
        {
            Assert.That(() => _sut.CheckIfTheUserToBlockIsTheCurrentUser("otherUserId"),
                Throws.Nothing);
        }

        [Test]
        public void GetUser_TheUserDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUser("userId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(user);

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void BlockUser_WhenCalled_ChangeUserStatusToBlocked()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.BlockUser(user.Object);

            user.Verify(x => x.ChangeUserStatus(It.IsAny<BlockedStatus>()));
        }

        [Test]
        public void UpdateUser_WhenCalled_UpdateUser()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUser(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }
    }
}