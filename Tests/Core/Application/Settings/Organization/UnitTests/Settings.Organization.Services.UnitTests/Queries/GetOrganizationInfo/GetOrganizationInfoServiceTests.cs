using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Settings.Organization.Services.Queries;
using Solen.Core.Domain.Subscription.Entities;

namespace Settings.Organization.Services.UnitTests.Queries.GetOrganizationInfo
{
    [TestFixture]
    public class GetOrganizationInfoServiceTests
    {
        private Mock<IGetOrganizationInfoRepository> _repo;
        private Mock<IOrganizationSubscriptionManager> _subscriptionManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetOrganizationInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetOrganizationInfoRepository>();
            _subscriptionManager = new Mock<IOrganizationSubscriptionManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetOrganizationInfoService(_repo.Object, _subscriptionManager.Object,
                _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetOrganizationName_WhenCalled_ReturnTheOrganizationName()
        {
            var organizationName = "organization name";
            _repo.Setup(x => x.GetOrganizationName("organizationId", default))
                .ReturnsAsync(organizationName);

            var result = _sut.GetOrganizationName(default).Result;

            Assert.That(result, Is.EqualTo(organizationName));
        }

        [Test]
        public void GetOrganizationSubscriptionPlan_WhenCalled_ReturnTheOrganizationSubscriptionPlan()
        {
            var plan = new SubscriptionPlan("id", "name", 1, 1, 1);
            _subscriptionManager.Setup(x => x.GetOrganizationSubscriptionPlan(default))
                .ReturnsAsync(plan);

            var result = _sut.GetOrganizationSubscriptionPlan(default).Result;

            Assert.That(result, Is.EqualTo(plan));
        }
        
        [Test]
        public void GetCurrentStorage_WhenCalled_ReturnTheOrganizationCurrentStorage()
        {
            var currentStorage = 1;
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default))
                .ReturnsAsync(currentStorage);

            var result = _sut.GetCurrentStorage(default).Result;

            Assert.That(result, Is.EqualTo(currentStorage));
        }
        
        [Test]
        public void GetCurrentUserCount_WhenCalled_ReturnTheOrganizationCurrentUserCount()
        {
            var currentUserCount = 1;
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentUserCount(default))
                .ReturnsAsync(currentUserCount);

            var result = _sut.GetCurrentUserCount(default).Result;

            Assert.That(result, Is.EqualTo(currentUserCount));
        }
    }
}