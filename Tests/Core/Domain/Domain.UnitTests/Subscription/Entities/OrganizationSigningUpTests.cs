using NUnit.Framework;
using Solen.Core.Domain.Subscription.Entities;

namespace Domain.UnitTests.Subscription.Entities
{
    [TestFixture]
    public class OrganizationSigningUpTests
    {
        private OrganizationSigningUp _sut;

        [Test]
        public void ConstructorWithEmailToken_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new OrganizationSigningUp("email", "token");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Email, Is.EqualTo("email"));
            Assert.That(_sut.Token, Is.EqualTo("token"));
        }
    }
}