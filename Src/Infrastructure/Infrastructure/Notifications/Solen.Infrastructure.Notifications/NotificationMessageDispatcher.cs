using System.Collections.Generic;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Infrastructure.Notifications
{
    public class NotificationMessageDispatcher : INotificationMessageDispatcher
    {

        private readonly IList<INotificationSender> _notificationSenders;

        public NotificationMessageDispatcher(IList<INotificationSender> notificationSenders)
        {
            _notificationSenders = notificationSenders ?? new List<INotificationSender>();
        }
        
        public async Task Dispatch(NotificationType notificationType, NotificationMessage message, RecipientContactInfo recipient)
        {
            foreach (var notificationSender in _notificationSenders)
                if (notificationSender.NotificationType.Name == notificationType.Name)
                    await notificationSender.Handle(message, recipient);
        }
    }
}