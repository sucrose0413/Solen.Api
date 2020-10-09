namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class PasswordForgottenEvent : NotificationEvent
    {
        public static readonly PasswordForgottenEvent Instance = new PasswordForgottenEvent();
        public PasswordForgottenEvent() : base(5, "PasswordForgottenEvent",
            "Send a notification to the user to change password")
        {
        }
    }
}