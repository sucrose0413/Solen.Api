using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Solen.Infrastructure.Notifications.Emailing.SendGrid
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public SendGridEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var from = new EmailAddress(_emailSettings.From);
            var to = new EmailAddress(email.RecipientAddress);
            var plainTextContent = email.Body;
            var htmlContent = email.Body;

            var msg = MailHelper.CreateSingleEmail(@from, to, email.Subject, plainTextContent, htmlContent);
            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("{0}", "SendEmailAsync Failed");
            }
        }
    }
}