using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Notifications.Services.Commands;
using Solen.Core.Domain.Notifications.Entities;

namespace Settings.Notifications.Services.UnitTests.Commands.ToggleNotificationActivation
{
    [TestFixture]
    public class ToggleNotificationActivationServiceTests
    {
        private Mock<IToggleNotificationActivationRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private ToggleNotificationActivationService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IToggleNotificationActivationRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new ToggleNotificationActivationService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void ActivateNotificationTemplate_TemplateIsAlreadyActivated_DoNoThing()
        {
            DisabledNotificationTemplate disabledTemplate = null;
            _repo.Setup(x => x.GetDisabledNotification("organizationId", "templateId", default))
                .ReturnsAsync(disabledTemplate);

            _sut.ActivateNotificationTemplate("templateId", default).Wait();

            _repo.Verify(x => x.RemoveDisabledNotification(disabledTemplate), Times.Never);
        }

        [Test]
        public void ActivateNotificationTemplate_TemplateIsNotActivatedYet_ActivateTheTemplate()
        {
            var disabledTemplate = new DisabledNotificationTemplate("organizationId", "templateId");
            _repo.Setup(x => x.GetDisabledNotification("organizationId", "templateId", default))
                .ReturnsAsync(disabledTemplate);

            _sut.ActivateNotificationTemplate("templateId", default).Wait();

            _repo.Verify(x => x.RemoveDisabledNotification(disabledTemplate));
        }

        [Test]
        public void DeactivateNotificationTemplate_TemplateIsAlreadyDeactivated_DoNoThing()
        {
            var disabledTemplate = new DisabledNotificationTemplate("organizationId", "templateId");
            _repo.Setup(x => x.GetDisabledNotification("organizationId", "templateId", default))
                .ReturnsAsync(disabledTemplate);

            _sut.DeactivateNotificationTemplate("templateId", default).Wait();

            _repo.Verify(x => x.AddDisabledNotification(disabledTemplate), Times.Never);
        }

        [Test]
        public void DeactivateNotificationTemplate_TemplateIsNotDeactivatedYet_DeactivateTheTemplate()
        {
            _repo.Setup(x => x.GetDisabledNotification("organizationId", "templateId", default))
                .ReturnsAsync((DisabledNotificationTemplate) null);

            _sut.DeactivateNotificationTemplate("templateId", default).Wait();

            _repo.Verify(x => x.AddDisabledNotification(It.Is<DisabledNotificationTemplate>(t =>
                t.OrganizationId == "organizationId" && t.NotificationTemplateId == "templateId")));
        }
    }
}