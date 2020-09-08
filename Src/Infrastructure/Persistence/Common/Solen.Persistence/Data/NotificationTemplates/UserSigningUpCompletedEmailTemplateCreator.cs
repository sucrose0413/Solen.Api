using System.Text;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Persistence.Data.NotificationTemplates
{
    public static class UserSigningUpCompletedEmailTemplateCreator
    {
        public static NotificationTemplate Create()
        {
            var template = new NotificationTemplate(new EmailNotification(),
                new UserSigningUpCompletedEvent(), isSystemNotification: true);

            template.UpdateTemplateSubject("Welcome to Solen LMS!");

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hi {{data.user_name}},");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("Welcome to  Solen LMS ! ");
            stringBuilder.Append("Your account has been successfully created.");
            stringBuilder.Append("<br/>");
            stringBuilder.Append("Enjoy your learning journey !");
            stringBuilder.Append("<br/> <br/>");
            
            template.UpdateTemplateBody(stringBuilder.ToString());

            return template;
        }
    }
}