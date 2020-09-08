using NUnit.Framework;
using Solen.Core.Domain.Common.Entities;

namespace Domain.UnitTests.Common.Entities
{
    [TestFixture]
    public class OrganizationTests
    {
        private Organization _sut;

        [Test]
        public void ConstructorWithNameSubscriptionPlanId_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new Organization("name", "subscriptionPlanId");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Name, Is.EqualTo("name"));
            Assert.That(_sut.SubscriptionPlanId, Is.EqualTo("subscriptionPlanId"));
        }

        [Test]
        public void UpdateName_WhenCalled_ChangeOrganizationName()
        {
            _sut = new Organization("old name", "subscriptionPlanId");

            _sut.UpdateName("new Name");

            Assert.That(_sut.Name, Is.EqualTo("new Name"));
        }
    }
}