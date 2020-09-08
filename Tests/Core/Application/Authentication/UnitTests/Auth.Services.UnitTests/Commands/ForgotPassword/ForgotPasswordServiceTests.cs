using MediatR;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Auth.Services.Commands;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.Services.UnitTests.Commands.ForgotPassword
{
    [TestFixture]
    public class ForgotPasswordServiceTests
    {
        private ForgotPasswordService _sut;
        private Mock<IUserManager> _userManager;
        private Mock<IRandomTokenGenerator> _tokenGenerator;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _tokenGenerator = new Mock<IRandomTokenGenerator>();
            _mediator = new Mock<IMediator>();

            _sut = new ForgotPasswordService(_userManager.Object, _tokenGenerator.Object, _mediator.Object);
        }

        [Test]
        public void GetUserFromRepo_UserDoesNotExist_ThrowUnknownEmailAddressException()
        {
            _userManager.Setup(x => x.FindByEmailAsync("email"))
                .ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserFromRepo("email", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUserFromRepo_UserIsBlocked_ThrowLockedException()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByEmailAsync("email")).ReturnsAsync(user);
            _userManager.Setup(x => x.IsActiveUser(user)).Returns(false);

            Assert.That(() => _sut.GetUserFromRepo("email", default),
                Throws.Exception.TypeOf<LockedException>());
        }

        [Test]
        public void GetUserFromRepo_UserIsActive_ReturnCorrectUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByEmailAsync("email")).ReturnsAsync(user);
            _userManager.Setup(x => x.IsActiveUser(user)).Returns(true);

            var result = _sut.GetUserFromRepo("email", default).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void SetUserPasswordToken_WhenCalled_SetUserPasswordToken()
        {
            var token = "token";
            _tokenGenerator.Setup(x => x.CreateToken(100)).Returns(token);
            var user = new Mock<User>("email", "organizationId");

            _sut.SetUserPasswordToken(user.Object);

            user.Verify(x => x.SetPasswordToken(token));
        }

        [Test]
        public void UpdateUserRepo_WhenCalled_UpdateUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUserRepo(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }

        [Test]
        public void PublishPasswordTokenSetEvent_WhenCalled_PublishPasswordTokenSetEvent()
        {
            var user = new User("email", "organizationId");

            _sut.PublishPasswordTokenSetEvent(user, default).Wait();

            _mediator.Verify(x =>
                x.Publish(It.Is<PasswordTokenSetEvent>(e => e.User == user),
                    default));
        }
    }
}