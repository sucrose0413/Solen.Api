using System;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Domain.Notifications.Entities
{
    public class Notification
    {
        private Notification()
        {
        }

        public Notification(NotificationEvent notificationEvent, string recipientId, string subject, string body)
        {
            Id = NotificationNewId;
            NotificationEvent = notificationEvent;
            RecipientId = recipientId;
            Subject = subject;
            Body = body;
            CreationDate = DateTime.Now;
            IsRead = false;
        }

        public string Id { get; private set; }
        public NotificationEvent NotificationEvent { get; private set; }
        public User Recipient { get; private set; }
        public string RecipientId { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public DateTime CreationDate { get; private set; }
        public bool IsRead { get; private set; }

        public virtual void MarkAsRead()
        {
            IsRead = true;
        }

        #region Private Methods

        private static string NotificationNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}