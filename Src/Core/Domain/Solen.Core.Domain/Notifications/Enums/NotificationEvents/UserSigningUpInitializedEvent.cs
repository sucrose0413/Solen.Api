namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class UserSigningUpInitializedEvent : NotificationEvent
    {
        public static readonly UserSigningUpInitializedEvent Instance = new UserSigningUpInitializedEvent();
        
        public UserSigningUpInitializedEvent() : base(3, "UserSigningUpInitializedEvent",
            "Send a notification to initialize a user sign up process")
        {
        }
    }
}