using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Organization.Services.Commands;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Settings.Organization.Services.UnitTests.Commands.UpdateOrganizationInfo
{
    [TestFixture]
    public class UpdateOrganizationInfoServiceTests
    {
        private Mock<IUpdateOrganizationInfoRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateOrganizationInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateOrganizationInfoRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateOrganizationInfoService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetOrganization_WhenCalled_ReturnTheOrganization()
        {
            var organization = new Org("name", "plan");
            _repo.Setup(x => x.GetOrganization("organizationId", default))
                .ReturnsAsync(organization);

            var result = _sut.GetOrganization(default).Result;

            Assert.That(result, Is.EqualTo(organization));
        }

        [Test]
        public void UpdateOrganizationName_WhenCalled_UpdateTheOrganizationName()
        {
            var organization = new Mock<Org>("name", "plan");

            _sut.UpdateOrganizationName(organization.Object, "new name");

            organization.Verify(x => x.UpdateName("new name"));
        }
        
        [Test]
        public void UpdateOrganizationRepo_WhenCalled_UpdateTheOrganizationRepo()
        {
            var organization = new Org("name", "plan");

            _sut.UpdateOrganizationRepo(organization);

            _repo.Verify(x => x.UpdateOrganization(organization));
        }
    }
}