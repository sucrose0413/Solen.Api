using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;

namespace Auth.Services.UnitTests.Queries.CheckPasswordToken
{
    [TestFixture]
    public class CheckPasswordTokenServiceTests
    {
        private CheckPasswordTokenService _sut;
        private Mock<IUserManager> _userManager;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();

            _sut = new CheckPasswordTokenService(_userManager.Object);
        }
        
        [Test]
        public void CheckPasswordToken_PasswordTokenDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.DoesPasswordTokenExist("invalidToken"))
                .ReturnsAsync(false);

            Assert.That(() => _sut.CheckPasswordToken("invalidToken", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void CheckPasswordToken_PasswordTokenDoesExist_ThrowNoException()
        {
            _userManager.Setup(x => x.DoesPasswordTokenExist("validToken"))
                .ReturnsAsync(true);

            Assert.That(() => _sut.CheckPasswordToken("validToken", default),
                Throws.Nothing);
        }
    }
}