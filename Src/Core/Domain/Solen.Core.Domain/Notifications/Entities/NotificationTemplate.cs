using Solen.Core.Domain.Common;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Core.Domain.Notifications.Entities
{
    public class NotificationTemplate
    {
        private NotificationTemplate()
        {
        }

        public NotificationTemplate(NotificationType type, NotificationEvent notificationEvent, bool isSystemNotification)
        {
            Id = $"{notificationEvent.Name}-{type.Name}";
            TypeName = type.Name;
            NotificationEventName = notificationEvent.Name;
            IsSystemNotification = isSystemNotification;
        }

        public string Id { get; private set; }
        public string TypeName { get; private set; }
        public string NotificationEventName { get; private set; }
        public string TemplateSubject { get; private set; }
        public string TemplateBody { get; private set; }
        public bool IsSystemNotification { get; private set; }

        public NotificationEvent NotificationEvent => Enumeration.FromName<NotificationEvent>(NotificationEventName);
        public NotificationType Type => Enumeration.FromName<NotificationType>(TypeName);

        public void UpdateTemplateSubject(string subject)
        {
            TemplateSubject = subject;
        }

        public void UpdateTemplateBody(string body)
        {
            TemplateBody = body;
        }
        
    }
}