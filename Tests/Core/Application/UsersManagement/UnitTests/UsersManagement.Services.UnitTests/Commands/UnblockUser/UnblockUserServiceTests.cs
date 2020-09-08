using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace UsersManagement.Services.UnitTests.Commands.UnblockUser
{
    [TestFixture]
    public class UnblockUserServiceTests
    {
        private Mock<IUserManager> _userManager;
        private UnblockUserService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _sut = new UnblockUserService(_userManager.Object);
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
        public void UnblockUser_WhenCalled_ChangeUserStatusToActive()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.UnblockUser(user.Object);

            user.Verify(x => x.ChangeUserStatus(It.IsAny<ActiveStatus>()));
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