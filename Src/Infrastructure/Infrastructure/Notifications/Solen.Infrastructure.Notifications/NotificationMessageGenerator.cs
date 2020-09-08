using System.Linq;
using DotLiquid;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Infrastructure.Notifications
{
    public class NotificationMessageGenerator : INotificationMessageGenerator
    {
        public NotificationMessage Generate(NotificationTemplate template, NotificationData data)
        {
            if (data == null)
                return new NotificationMessage(template.TemplateSubject, template.TemplateBody,
                    template.NotificationEvent);

            RegisterType(data);

            var messageSubject = GenerateMessageFromTemplate(template.TemplateSubject, data);
            var messageBody = GenerateMessageFromTemplate(template.TemplateBody, data);

            return new NotificationMessage(messageSubject, messageBody, template.NotificationEvent);
        }

        #region Private Methods

        private static string GenerateMessageFromTemplate(string template, NotificationData data)
        {
            var parsedSubject = Template.Parse(template);
            return parsedSubject.Render(Hash.FromAnonymousObject(new {data}));
        }

        private static void RegisterType(NotificationData data)
        {
            var type = data.GetType();

            var names = type
                .GetProperties()
                .Select(p => p.Name)
                .ToArray();

            Template.RegisterSafeType(type, names);
        }

        #endregion
    }
}