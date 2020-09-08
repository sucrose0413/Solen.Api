using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Core.Domain.Subscription.Entities;

namespace Dashboard.Services.UnitTests.Queries.GetStorageInfo
{
    [TestFixture]
    public class GetStorageInfoServiceTests
    {
        private Mock<IOrganizationSubscriptionManager> _subscriptionManager;
        private GetStorageInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _subscriptionManager = new Mock<IOrganizationSubscriptionManager>();
            _sut = new GetStorageInfoService(_subscriptionManager.Object);
        }

        [Test]
        public void GetStorageInfo_WhenCalled_ReturnStorageInfo()
        {
            var subscriptionPlan = new SubscriptionPlan("id", "name", 100, 10, 5);
            _subscriptionManager.Setup(x => x.GetOrganizationSubscriptionPlan(default))
                .ReturnsAsync(subscriptionPlan);
            var currentStorage = 1;
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default))
                .ReturnsAsync(currentStorage);

            var result = _sut.GetStorageInfo(default).Result;

            Assert.That(result.MaximumStorage, Is.EqualTo(subscriptionPlan.MaxStorage));
            Assert.That(result.CurrentStorage, Is.EqualTo(currentStorage));
        }
    }
}