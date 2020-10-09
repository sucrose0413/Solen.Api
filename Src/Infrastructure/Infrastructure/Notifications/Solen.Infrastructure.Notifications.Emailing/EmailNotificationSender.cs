using System.Threading.Tasks;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Infrastructure.Notifications.Emailing
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly IEmailSender _emailSender;

        public EmailNotificationSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(NotificationMessage message, RecipientContactInfo recipient)
        {
            var email = new Email(recipient.Email, message.Subject, message.Body);
            await _emailSender.SendEmailAsync(email);
        }

        public NotificationType NotificationType => EmailNotification.Instance;
    }
}