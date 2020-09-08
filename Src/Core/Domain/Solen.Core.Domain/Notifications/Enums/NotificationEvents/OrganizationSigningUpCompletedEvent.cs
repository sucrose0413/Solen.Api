namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class OrganizationSigningUpCompletedEvent : NotificationEvent
    {
        public OrganizationSigningUpCompletedEvent() : base(2, "OrganizationSigningUpCompletedEvent",
            "Send a notification to confirm the organization account creation.")
        {
        }
    }
}