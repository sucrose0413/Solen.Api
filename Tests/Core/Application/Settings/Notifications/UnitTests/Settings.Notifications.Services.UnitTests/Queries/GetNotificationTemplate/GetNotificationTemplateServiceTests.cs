using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.Core.Application.Settings.Notifications.Services.Queries;

namespace Settings.Notifications.Services.UnitTests.Queries.GetNotificationTemplate
{
    [TestFixture]
    public class GetNotificationTemplateServiceTests
    {
        private Mock<IGetNotificationTemplateRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetNotificationTemplateService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetNotificationTemplateRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetNotificationTemplateService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetNotificationTemplate_TemplateDoesNotExist_ThrowNotFoundException()
        {
            var disabledTemplates = new List<string>();
            _repo.Setup(x => x.GetDisabledNotifications("organizationId", default))
                .ReturnsAsync(disabledTemplates);
            _repo.Setup(x => x.GetNotificationTemplate("templateId", disabledTemplates, default))
                .ReturnsAsync((NotificationTemplateDto) null);


            Assert.That(() => _sut.GetNotificationTemplate("templateId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetNotificationTemplate_TemplateDoesExist_ReturnTheTemplate()
        {
            var disabledTemplates = new List<string>();
            _repo.Setup(x => x.GetDisabledNotifications("organizationId", default))
                .ReturnsAsync(disabledTemplates);
            var template = new NotificationTemplateDto();
            _repo.Setup(x => x.GetNotificationTemplate("templateId", disabledTemplates, default))
                .ReturnsAsync(template);

            var result = _sut.GetNotificationTemplate("templateId", default).Result;

            Assert.That(result, Is.EqualTo(template));
        }
    }
}