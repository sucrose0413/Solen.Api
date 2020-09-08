using System;

namespace Solen.Core.Application.Notifications.Queries
{
    public class NotificationDto
    {
        public NotificationDto(string id, string notificationEvent, string subject, string body, DateTime creationDate, bool isRead)
        {
            Id = id;
            NotificationEvent = notificationEvent;
            Subject = subject;
            Body = body;
            CreationDate = creationDate;
            IsRead = isRead;
        }

        public string Id { get; }
        public string NotificationEvent { get; }
        public string Subject { get; }
        public string Body { get; }
        public DateTime CreationDate { get; }
        public bool IsRead { get; }
    }
}