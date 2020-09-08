namespace Solen.Infrastructure.Notifications.Emailing
{
    public class EmailSettings
    {
        public string ApiKey { get; set; }
        public string From { get; set; }
        public bool IsPickupDirectory { get; set; }
        public string PickupDirectory { get; set; }
    }
}