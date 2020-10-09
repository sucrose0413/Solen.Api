namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class OrganizationSigningUpCompletedEvent : NotificationEvent
    {
        public static readonly OrganizationSigningUpCompletedEvent Instance = new OrganizationSigningUpCompletedEvent();
        public OrganizationSigningUpCompletedEvent() : base(2, "OrganizationSigningUpCompletedEvent",
            "Send a notification to confirm the organization account creation.")
        {
        }
    }
}