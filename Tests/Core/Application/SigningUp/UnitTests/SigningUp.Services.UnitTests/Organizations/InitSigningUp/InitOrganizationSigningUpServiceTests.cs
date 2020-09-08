using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Services.Organizations;
using Solen.Core.Domain.Subscription.Entities;

namespace SigningUp.Services.UnitTests.Organizations.InitSigningUp
{
    [TestFixture]
    public class InitOrganizationSigningUpServiceTests
    {
        private Mock<IInitOrganizationSigningUpRepository> _repo;
        private Mock<IUserManager> _userManager;
        private Mock<IRandomTokenGenerator> _tokenGenerator;
        private Mock<ISecurityConfig> _securityConfig;
        private InitOrganizationSigningUpService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IInitOrganizationSigningUpRepository>();
            _userManager = new Mock<IUserManager>();
            _tokenGenerator = new Mock<IRandomTokenGenerator>();
            _securityConfig = new Mock<ISecurityConfig>();
            _sut = new InitOrganizationSigningUpService(_repo.Object, _userManager.Object, _tokenGenerator.Object,
                _securityConfig.Object);
        }

        [Test]
        public void CheckIfSigningUpIsEnabled_SigningUpIsNotEnabled_ThrowSigningUpNotEnabledException()
        {
            _securityConfig.Setup(x => x.IsSigninUpEnabled).Returns(false);

            Assert.That(() => _sut.CheckIfSigningUpIsEnabled(),
                Throws.Exception.TypeOf<SigningUpNotEnabledException>());
        }

        [Test]
        public void CheckIfSigningUpIsEnabled_SigningUpIsEnabled_ThrowNoException()
        {
            _securityConfig.Setup(x => x.IsSigninUpEnabled).Returns(true);

            Assert.That(() => _sut.CheckIfSigningUpIsEnabled(), Throws.Nothing);
        }

        [Test]
        public void CheckEmailExistence_EmailAlreadyRegistered_ThrowEmailAlreadyRegisteredException()
        {
            _userManager.Setup(x => x.DoesEmailExistAsync("email@example.com"))
                .ReturnsAsync(true);

            Assert.That(() => _sut.CheckEmailExistence("email@example.com"),
                Throws.Exception.TypeOf<EmailAlreadyRegisteredException>());
        }

        [Test]
        public void CheckEmailExistence_EmailIsNotRegistered_ThrowNoException()
        {
            _userManager.Setup(x => x.DoesEmailExistAsync("email@example.com"))
                .ReturnsAsync(false);

            Assert.That(() => _sut.CheckEmailExistence("email@example.com"), Throws.Nothing);
        }

        [Test]
        public void InitOrganizationSigningUp_WhenCalled_CreateOrganizationSigningUp()
        {
            var token = "signing up token";
            _tokenGenerator.Setup(x => x.CreateToken(100)).Returns(token);

            var result = _sut.InitOrganizationSigningUp("email@example.com");

            Assert.That(result.Email, Is.EqualTo("email@example.com"));
            Assert.That(result.Token, Is.EqualTo(token));
        }

        [Test]
        public void AddOrganizationSigningUpToRepo_WhenCalled_AddTheSigningUpToRepo()
        {
            var signingUp = new OrganizationSigningUp("email", "token");

            _sut.AddOrganizationSigningUpToRepo(signingUp, default).Wait();

            _repo.Verify(x => x.AddOrganizationSigningUp(signingUp, default));
        }
    }
}