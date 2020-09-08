using System.Collections.Generic;
using System.Linq;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Persistence.Common.Notifications
{
    public class NotificationsRepo : INotificationsRepo
    {
        private readonly SolenDbContext _context;

        public NotificationsRepo(SolenDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetDisabledNotifications(string organizationId)
        {
            return _context.DisabledNotificationTemplates
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => x.NotificationTemplateId).ToList();
        }

        public IList<NotificationTemplate> GetNotificationTemplatesByNotificationEvent(
            NotificationEvent notificationEvent, IEnumerable<string> excludedTemplates)
        {
            return _context.NotificationTemplates
                .Where(x => x.NotificationEventName == notificationEvent.Name
                            && excludedTemplates.All(t => t != x.Id))
                .ToList();
        }

        public void AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
        }
    }
}