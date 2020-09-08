using System;
using NUnit.Framework;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Domain.UnitTests.Security.Entities
{
    [TestFixture]
    public class RefreshTokenTests
    {
        private RefreshToken _sut;

        [Test]
        public void ConstructorWithUserIdExpiryTime_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            var user = new User("email", "organizationId");
            var now = DateTime.UtcNow;
            _sut = new RefreshToken(user, now);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.UserId, Is.EqualTo(user.Id));
            Assert.That(_sut.ExpiryTime, Is.EqualTo(now));
        }
    }
}