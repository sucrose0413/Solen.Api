using System.Text;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Persistence.Data.NotificationTemplates
{
    public static class CoursePublishedPushTemplateCreator
    {
        public static NotificationTemplate Create()
        {
            var template = new NotificationTemplate(PushNotification.Instance, 
                CoursePublishedEvent.Instance, isSystemNotification: false);

            template.UpdateTemplateSubject("A course has been published !");

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hi,");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("The course «{{data.course_name}}» has just been published by {{data.creator_name}}.");
            stringBuilder.Append("<br/> <br/>");
            stringBuilder.Append("Enjoy your training course.");
            stringBuilder.Append("<br/> <br/>");
            
            template.UpdateTemplateBody(stringBuilder.ToString());

            return template;
        }
    }
}