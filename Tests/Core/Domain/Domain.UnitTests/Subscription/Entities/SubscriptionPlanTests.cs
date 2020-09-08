using NUnit.Framework;
using Solen.Core.Domain.Subscription.Entities;

namespace Domain.UnitTests.Subscription.Entities
{
    [TestFixture]
    public class SubscriptionPlanTests
    {
        private SubscriptionPlan _sut;
        
        [Test]
        public void ConstructorWithIdNameMaxStorageMaxUsers_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new SubscriptionPlan("id", "name", 100, 1, 10);

            Assert.That(_sut.Id, Is.EqualTo("id"));
            Assert.That(_sut.Name, Is.EqualTo("name"));
            Assert.That(_sut.MaxStorage, Is.EqualTo(100));
            Assert.That(_sut.MaxFileSize, Is.EqualTo(1));
            Assert.That(_sut.MaxUsers, Is.EqualTo(10));
        }
    }
}