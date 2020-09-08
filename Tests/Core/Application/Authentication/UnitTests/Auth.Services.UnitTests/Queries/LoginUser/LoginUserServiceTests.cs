using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.Services.UnitTests.Queries.LoginUser
{
    [TestFixture]
    public class LoginUserServiceTests
    {
        private LoginUserService _sut;
        private Mock<IUserManager> _userManager;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();

            _sut = new LoginUserService(_userManager.Object);
        }

        [Test]
        public void GetUserByEmail_UserDoesNotExist_ThrowUnauthorizedException()
        {
            _userManager.Setup(x => x.FindByEmailAsync("invalidEmail"))
                .ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserByEmail("invalidEmail", default),
                Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public void GetUserByEmail_UserDoesExist_ThrowNoException()
        {
            _userManager.Setup(x => x.FindByEmailAsync("email"))
                .ReturnsAsync(new User("email", "org"));

            Assert.That(() => _sut.GetUserByEmail("email", default), Throws.Nothing);
        }

        [Test]
        public void CheckIfPasswordIsInvalid_PasswordIsInvalid_ThrowUnauthorizedException()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.CheckPassword(user, "wrongPassword"))
                .Returns(false);

            Assert.That(() => _sut.CheckIfPasswordIsInvalid(user, "wrongPassword"),
                Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public void CheckIfPasswordIsInvalid_PasswordIsValid_ThrowNoException()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.CheckPassword(user, "password"))
                .Returns(true);

            Assert.That(() => _sut.CheckIfPasswordIsInvalid(user, "password"),
                Throws.Nothing);
        }
    }
}