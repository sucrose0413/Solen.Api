using Solen.Core.Domain.Common.Entities;

namespace Solen.Core.Domain.Notifications.Entities
{
    public class DisabledNotificationTemplate
    {
        private DisabledNotificationTemplate()
        {
        }
        
        public DisabledNotificationTemplate(string organizationId, string notificationTemplateId)
        {
            OrganizationId = organizationId;
            NotificationTemplateId = notificationTemplateId;
        }

        public string OrganizationId { get; private set; }
        public Organization Organization { get; set; }
        public string NotificationTemplateId { get; private set; }
        public NotificationTemplate NotificationTemplate { get; set; }
    }
}