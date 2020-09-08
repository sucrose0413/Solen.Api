using Moq;
using NUnit.Framework;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Services.Organizations.Queries;

namespace SigningUp.Services.UnitTests.Organizations.CheckOrganizationSigningUpToken
{
    [TestFixture]
    public class CheckOrganizationSigningUpTokenServiceTests
    {
        private Mock<ICheckOrganizationSigningUpTokenRepository> _repo;
        private CheckOrganizationSigningUpTokenService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICheckOrganizationSigningUpTokenRepository>();
            _sut = new CheckOrganizationSigningUpTokenService(_repo.Object);
        }
        
        [Test]
        public void CheckSigningUpToken_SigninUpTokenIsNotFoundOrInvalid_ThrowNotFoundException()
        {
            _repo.Setup(x => x.DoesSigningUpTokenExist("token", default))
                .ReturnsAsync(false);

            Assert.That(() => _sut.CheckSigningUpToken("token", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void CheckSigningUpToken_SigninUpTokenIsValid_ThrowNoException()
        {
            _repo.Setup(x => x.DoesSigningUpTokenExist("token", default))
                .ReturnsAsync(true);

            Assert.That(() => _sut.CheckSigningUpToken("token", default), Throws.Nothing);
        }
    }
}