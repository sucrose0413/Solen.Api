using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Subscription.Constants;

namespace SigningUp.UnitTests.Organizations.Commands.CompleteSigningUp
{
    [TestFixture]
    public class SigningUpCompletedEventTests
    {
        private SigningUpCompletedEvent _sut;

        [Test]
        public void ConstructorWithOrganizationAndUser_WhenCalled_SetPropertiesCorrectly()
        {
            var organization = new Organization("organization name", SubscriptionPlans.Free);
            var user = new User("email", organization.Id);
            
            _sut = new SigningUpCompletedEvent(organization, user);

            Assert.That(_sut.Organization, Is.EqualTo(organization));
            Assert.That(_sut.User, Is.EqualTo(user));
        }
    }
}