using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Solen.Infrastructure.Notifications.Emailing.Pickup
{
    public class EmlEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmlEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(Email email)
        {
            var mail = new MailMessage(new MailAddress(_emailSettings.From), new MailAddress(email.RecipientAddress))
            {
                Subject = email.Subject, Body = email.Body, IsBodyHtml = true
            };
            
            var client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = _emailSettings.PickupDirectory
            };
            client.Send(mail);

            return Task.CompletedTask;
        }
    }
}