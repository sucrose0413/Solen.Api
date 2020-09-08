using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.Core.Application.Settings.Notifications.Services.Queries;

namespace Settings.Notifications.Services.UnitTests.Queries.GetNotificationTemplates
{
    [TestFixture]
    public class GetNotificationTemplatesServiceTests
    {
        private Mock<IGetNotificationTemplatesRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetNotificationTemplatesService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetNotificationTemplatesRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetNotificationTemplatesService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetNotificationTemplates_WhenCalled_ReturnTheTemplatesList()
        {
            var disabledTemplates = new List<string>();
            _repo.Setup(x => x.GetDisabledNotifications("organizationId", default))
                .ReturnsAsync(disabledTemplates);
            var templates = new List<NotificationTemplateDto>();
            _repo.Setup(x => x.GetNotificationTemplates(disabledTemplates, default))
                .ReturnsAsync(templates);

            var result = _sut.GetNotificationTemplates(default).Result;

            Assert.That(result, Is.EqualTo(templates));
        }
    }
}