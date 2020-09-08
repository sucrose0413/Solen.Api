namespace Solen.Infrastructure.Notifications.Emailing
{
    public class Email
    {
        public Email(string recipientAddress, string subject, string body)
        {
            RecipientAddress = recipientAddress;
            Subject = subject;
            Body = body;
        }

        public string RecipientAddress { get; }
        public string Subject { get; }
        public string Body { get; }
    }
}