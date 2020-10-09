namespace Solen.Core.Domain.Notifications.Enums.NotificationTypes
{
    public class EmailNotification : NotificationType
    {
        public static readonly EmailNotification Instance = new EmailNotification();

        public EmailNotification() : base(1, "Email")
        {
        }
    }
}