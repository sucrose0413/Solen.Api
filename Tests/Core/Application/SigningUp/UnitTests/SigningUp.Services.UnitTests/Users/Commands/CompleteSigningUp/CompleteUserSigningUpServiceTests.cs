using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Services.Users.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace SigningUp.Services.UnitTests.Users.Commands.CompleteSigningUp
{
    [TestFixture]
    public class CompleteUserSigningUpServiceTests
    {
        private Mock<IUserManager> _userManager;
        private CompleteUserSigningUpService _sut;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _sut = new CompleteUserSigningUpService(_userManager.Object);
        }

        [Test]
        public void GetUserByInvitationToken_InvitationTokenIsNotFoundOrInvalid_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByInvitationTokenAsync("token"))
                .ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserByInvitationToken("token"),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUserByInvitationToken_InvitationTokenIsValid_ReturnTheUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByInvitationTokenAsync("token"))
                .ReturnsAsync(user);

            var result = _sut.GetUserByInvitationToken("token").Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void UpdateUserName_WhenCalled_UpdateTheUserName()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.UpdateUserName(user.Object, "user name");

            user.Verify(x => x.UpdateUserName("user name"));
        }

        [Test]
        public void ValidateUserInscription_WhenCalled_ValidateTheUserInscription()
        {
            var user = new User("email", "organizationId");
            var password = "password";

            _sut.ValidateUserInscription(user, password);

            _userManager.Verify(x => x.UpdatePassword(user, password));
            _userManager.Verify(x => x.ValidateUserInscription(user));
        }

        [Test]
        public void UpdateUserRepo_WhenCalled_UpdateUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUserRepo(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }
    }
}