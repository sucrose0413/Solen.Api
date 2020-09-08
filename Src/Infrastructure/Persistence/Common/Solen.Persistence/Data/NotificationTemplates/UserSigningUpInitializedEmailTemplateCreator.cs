using System.Text;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Persistence.Data.NotificationTemplates
{
    public static class UserSigningUpInitializedEmailTemplateCreator
    {
        public static NotificationTemplate Create()
        {
            var template = new NotificationTemplate(new EmailNotification(),
                new UserSigningUpInitializedEvent(), isSystemNotification: true);

            template.UpdateTemplateSubject("{{data.invited_by}} has invited you to join Solen LMS");

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hi,");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("{{data.invited_by}} has invited you to join Solen LMS.");
            stringBuilder.Append("<br/>");
            stringBuilder.Append("Please click on the link below to complete the signing up process :");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("{{ data.link_to_complete_signing_up }}");
            stringBuilder.Append("<br/> <br/>");

            template.UpdateTemplateBody(stringBuilder.ToString());

            return template;
        }
    }
}