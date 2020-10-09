using System.Text;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Persistence.Data.NotificationTemplates
{
    public static class PasswordForgottenEmailTemplateCreator
    {
        public static NotificationTemplate Create()
        {
            var template = new NotificationTemplate(EmailNotification.Instance,
                PasswordForgottenEvent.Instance, isSystemNotification: true);

            template.UpdateTemplateSubject("Reset Password");

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hi,");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("This is a system message in reply to your request to change your password.");
            stringBuilder.Append("<br/>");
            stringBuilder.Append("To reset your password, please open the link below ");
            stringBuilder.Append("and follow the instructions on the page : ");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("{{ data.link_to_reset_password }}");
            stringBuilder.Append("<br/> <br/>");

            template.UpdateTemplateBody(stringBuilder.ToString());

            return template;
        }
    }
}