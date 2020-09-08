using Moq;
using NUnit.Framework;
using Solen.Core.Application.Settings.Organization.Queries;
using Solen.Core.Domain.Subscription.Entities;

namespace Settings.Organization.UnitTests.Queries.GetOrganizationInfo
{
    [TestFixture]
    public class GetOrganizationInfoQueryHandlerTests
    {
        private GetOrganizationInfoQueryHandler _sut;
        private Mock<IGetOrganizationInfoService> _service;
        private GetOrganizationInfoQuery _query;

        private SubscriptionPlan _subscriptionPlan;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetOrganizationInfoService>();
            _sut = new GetOrganizationInfoQueryHandler(_service.Object);

            _query = new GetOrganizationInfoQuery();

            _subscriptionPlan = new SubscriptionPlan("id", "plan", 1, 1, 1);
            _service.Setup(x => x.GetOrganizationSubscriptionPlan(default))
                .ReturnsAsync(_subscriptionPlan);
        }

        [Test]
        public void WhenCalled_ReturnOrganizationName()
        {
            var organizationName = "organizationName";
            _service.Setup(x => x.GetOrganizationName(default))
                .ReturnsAsync(organizationName);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.OrganizationName, Is.EqualTo(organizationName));
        }

        [Test]
        public void WhenCalled_ReturnSubscriptionPlanName()
        {
            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.SubscriptionPlan, Is.EqualTo(_subscriptionPlan.Name));
        }

        [Test]
        public void WhenCalled_ReturnMaxStorage()
        {
            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.MaxStorage, Is.EqualTo(_subscriptionPlan.MaxStorage));
        }

        [Test]
        public void WhenCalled_ReturnCurrentStorage()
        {
            var currentStorage = 1;
            _service.Setup(x => x.GetCurrentStorage(default))
                .ReturnsAsync(currentStorage);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CurrentStorage, Is.EqualTo(currentStorage));
        }

        [Test]
        public void WhenCalled_ReturnCurrentUserCount()
        {
            var currentUserCount = 1;
            _service.Setup(x => x.GetCurrentUserCount(default))
                .ReturnsAsync(currentUserCount);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CurrentUsersCount, Is.EqualTo(currentUserCount));
        }
    }
}