using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Core.Domain.Subscription.Entities;

namespace Dashboard.Services.UnitTests.Queries.GetUserCountInfo
{
    [TestFixture]
    public class GetUserCountInfoServiceTests
    {
        private Mock<IOrganizationSubscriptionManager> _subscriptionManager;
        private GetUserCountInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _subscriptionManager = new Mock<IOrganizationSubscriptionManager>();
            _sut = new GetUserCountInfoService(_subscriptionManager.Object);
        }

        [Test]
        public void GetUserCountInfo_WhenCalled_ReturnUserCountInfo()
        {
            var subscriptionPlan = new SubscriptionPlan("id", "name", 100, 10, 5);
            _subscriptionManager.Setup(x => x.GetOrganizationSubscriptionPlan(default))
                .ReturnsAsync(subscriptionPlan);
            var currentUserCount = 1;
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentUserCount(default))
                .ReturnsAsync(currentUserCount);

            var result = _sut.GetUserCountInfo(default).Result;

            Assert.That(result.MaximumUsers, Is.EqualTo(subscriptionPlan.MaxUsers));
            Assert.That(result.CurrentUserCount, Is.EqualTo(currentUserCount));
        }
    }
}