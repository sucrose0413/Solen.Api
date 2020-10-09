using System.Text;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Persistence.Data.NotificationTemplates
{
    public static class OrgSigningUpCompletedEmailTemplateCreator
    {
        public static NotificationTemplate Create()
        {
            var template = new NotificationTemplate(EmailNotification.Instance,
                OrganizationSigningUpCompletedEvent.Instance, isSystemNotification: true);

            template.UpdateTemplateSubject("Welcome to Solen LMS!");

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hi {{data.user_name}},");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("Thank you for your interest in Solen LMS.");
            stringBuilder.Append("Your account has been successfully created.");
            stringBuilder.Append("<br/>");
            stringBuilder.Append("It’s great to have you here! We hope you and your learners will ");
            stringBuilder.Append("enjoy using this platform.");
            stringBuilder.Append("<br/> <br/>");

            template.UpdateTemplateBody(stringBuilder.ToString());

            return template;
        }
    }
}