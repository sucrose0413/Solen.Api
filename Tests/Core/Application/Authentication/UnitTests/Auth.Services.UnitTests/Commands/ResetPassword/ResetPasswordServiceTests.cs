using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Commands;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.Services.UnitTests.Commands.ResetPassword
{
    [TestFixture]
    public class ResetPasswordServiceTests
    {
        private ResetPasswordService _sut;
        private Mock<IUserManager> _userManager;


        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();

            _sut = new ResetPasswordService(_userManager.Object);
        }

        [Test]
        public void GetUserByPasswordToken_UserDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByPasswordTokenAsync("email"))
                .ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserByPasswordToken("email", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void GetUserByPasswordToken_UserDoesExist_ReturnTheCorrectUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByPasswordTokenAsync("token"))
                .ReturnsAsync(user);

            var result = _sut.GetUserByPasswordToken("token", default).Result;
            
            Assert.That(result, Is.EqualTo(user));
        }
        
        [Test]
        public void UpdateUserPassword_WhenCalled_UpdateUserPassword()
        {
            var user = new User("email", "organizationId");

           _sut.UpdateUserPassword(user, "password");
            
           _userManager.Verify(x => x.UpdatePassword(user, "password"));
        }
        
        [Test]
        public void InitUserPasswordToken_WhenCalled_InitUserPasswordToken()
        {
            var user = new Mock<User>("email", "organizationId");

            _sut.InitUserPasswordToken(user.Object);

            user.Verify(x => x.InitPasswordToken());
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