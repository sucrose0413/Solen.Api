using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Common.Notifications.Impl
{
    public interface INotificationMessageGenerator
    {
        NotificationMessage Generate(NotificationTemplate template, NotificationData data);
    }
}