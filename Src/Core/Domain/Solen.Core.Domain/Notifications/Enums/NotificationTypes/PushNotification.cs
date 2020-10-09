namespace Solen.Core.Domain.Notifications.Enums.NotificationTypes
{
    public class PushNotification : NotificationType
    {
        public static readonly PushNotification Instance = new PushNotification();

        public PushNotification() : base(2, "Push")
        {
        }
    }
}