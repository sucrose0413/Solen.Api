namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class UserSigningUpCompletedEvent : NotificationEvent
    {
        public static readonly UserSigningUpCompletedEvent Instance = new UserSigningUpCompletedEvent();
        
        public UserSigningUpCompletedEvent() : base(4, "UserSigningUpCompletedEvent",
            "Send a notification to confirm the a user account creation")
        {
        }
    }
}