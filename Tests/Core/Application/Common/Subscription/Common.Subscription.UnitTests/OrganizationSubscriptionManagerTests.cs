using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Common.Subscription.Impl;
using Solen.Core.Domain.Subscription.Entities;

namespace Common.Subscription.UnitTests
{
    [TestFixture]
    public class OrganizationSubscriptionManagerTests
    {
        private OrganizationSubscriptionManager _sut;
        private Mock<IOrganizationSubscriptionRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IOrganizationSubscriptionRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new OrganizationSubscriptionManager(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetOrganizationSubscriptionPlan_WhenCalled_ReturnOrganizationSubscriptionPlan()
        {
            var subscriptionPlan = new SubscriptionPlan("id", "name", 1, 1, 1);
            _repo.Setup(x => x.GetOrganizationSubscriptionPlan("organizationId", default))
                .ReturnsAsync(subscriptionPlan);

            var result = _sut.GetOrganizationSubscriptionPlan(default).Result;

            Assert.That(result, Is.EqualTo(subscriptionPlan));
        }

        [Test]
        public void GetOrganizationCurrentStorage_WhenCalled_ReturnOrganizationCurrentStorage()
        {
            var currentStorage = 1;
            _repo.Setup(x => x.GetOrganizationCurrentStorage("organizationId", default))
                .ReturnsAsync(currentStorage);

            var result = _sut.GetOrganizationCurrentStorage(default).Result;

            Assert.That(result, Is.EqualTo(currentStorage));
        }

        [Test]
        public void GetOrganizationCurrentUserCount_WhenCalled_ReturnOrganizationCurrentUserCount()
        {
            var userCount = 1;
            _repo.Setup(x => x.GetOrganizationCurrentUsersCount("organizationId", default))
                .ReturnsAsync(userCount);

            var result = _sut.GetOrganizationCurrentUserCount(default).Result;

            Assert.That(result, Is.EqualTo(userCount));
        }
    }
}