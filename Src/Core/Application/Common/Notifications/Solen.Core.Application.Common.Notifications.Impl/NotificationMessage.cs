using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Common.Notifications.Impl
{
    public class NotificationMessage
    {
        public NotificationMessage(string subject, string body, NotificationEvent notificationEvent)
        {
            Subject = subject;
            Body = body;
            NotificationEvent = notificationEvent;
        }
        public string Subject { get; }
        public string Body { get; }
        public NotificationEvent NotificationEvent { get; }
    }
}