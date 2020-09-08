namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public class NotificationTemplateDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Event { get; set; }
        public bool IsActivated { get; set; }
    }
}