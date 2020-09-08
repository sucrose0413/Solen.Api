using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Auth.Services.UnitTests.Queries.RefreshTokenServiceTests
{
    [TestFixture]
    public class RefreshTokenServiceTests
    {
        private RefreshTokenService _sut;
        private Mock<IRefreshTokenRepository> _repo;
        private Mock<IDateTime> _dateTime;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IRefreshTokenRepository>();
            _dateTime = new Mock<IDateTime>();

            _sut = new RefreshTokenService(_repo.Object, _dateTime.Object);
        }

        [Test]
        public void GetCurrentRefreshToken_RefreshTokenDoesNotExist_ThrowUnauthorizedException()
        {
            _repo.Setup(x => x.GetRefreshToken("invalidToken", default))
                .ReturnsAsync((RefreshToken) null);

            Assert.That(() => _sut.GetCurrentRefreshToken("invalidToken", default),
                Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public void GetCurrentRefreshToken_RefreshTokenDoesExist_ThrowNoException()
        {
            var user = new User("email", "organizationId");
            var refreshToken = new RefreshToken(user, null);
            _repo.Setup(x => x.GetRefreshToken("token", default))
                .ReturnsAsync(refreshToken);

            Assert.That(() => _sut.GetCurrentRefreshToken("token", default),
                Throws.Nothing);
        }

        [Test]
        public void CheckRefreshTokenValidity_RefreshTokenExpired_ThrowUnauthorizedException()
        {
            var now = DateTime.UtcNow;
            _dateTime.Setup(x => x.UtcNow).Returns(now);
            var user = new User("email", "organizationId");
            var expiredRefreshToken = new RefreshToken(user, now.AddDays(-1));


            Assert.That(() => _sut.CheckRefreshTokenValidity(expiredRefreshToken),
                Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public void CheckRefreshTokenValidity_RefreshTokenHasNotExpiredYet_ThrowNoException()
        {
            var now = DateTime.UtcNow;
            _dateTime.Setup(x => x.UtcNow).Returns(now);
            var user = new User("email", "organizationId");
            var expiredRefreshToken = new RefreshToken(user, now.AddDays(1));


            Assert.That(() => _sut.CheckRefreshTokenValidity(expiredRefreshToken),
                Throws.Nothing);
        }

        [Test]
        public void CheckRefreshTokenValidity_RefreshTokenWithNoExpiryTime_ThrowNoException()
        {
            var user = new User("email", "organizationId");
            var expiredRefreshToken = new RefreshToken(user, null);


            Assert.That(() => _sut.CheckRefreshTokenValidity(expiredRefreshToken),
                Throws.Nothing);
        }

        [Test]
        public void GetUserByRefreshToken_WhenCalled_GetUserByRefreshToken()
        {
            var refreshToken = "refreshToken";
            var user = new User("userId", "organizationId");
            _repo.Setup(x => x.GetUserByRefreshToken(refreshToken, default))
                .ReturnsAsync(user);

            var result = _sut.GetUserByRefreshToken(refreshToken, default).Result;

            Assert.That(result, Is.EqualTo(user));
        }
    }
}