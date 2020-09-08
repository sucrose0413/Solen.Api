using System.Threading.Tasks;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Infrastructure.Notifications
{
    public interface INotificationSender
    {
        Task Handle(NotificationMessage message, RecipientContactInfo recipient);
        NotificationType NotificationType { get; }
    }
}