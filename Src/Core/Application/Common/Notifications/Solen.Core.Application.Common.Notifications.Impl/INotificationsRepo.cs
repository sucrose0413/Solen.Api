using System.Collections.Generic;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Common.Notifications.Impl
{
    public interface INotificationsRepo
    {
        IEnumerable<string> GetDisabledNotifications(string organizationId);

        IList<NotificationTemplate> GetNotificationTemplatesByNotificationEvent(NotificationEvent notificationEvent,
            IEnumerable<string> excludedTemplates);

        void AddNotification(Notification notification);
    }
}