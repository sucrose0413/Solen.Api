namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class OrganizationSigningUpInitializedEvent : NotificationEvent
    {
        public OrganizationSigningUpInitializedEvent() :
            base(1, "OrganizationSigningUpInitializedEvent",
                "Send a notification to initialize an organization sign up process")
        {
        }
    }
}