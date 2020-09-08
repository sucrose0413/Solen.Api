using System.Threading.Tasks;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Core.Application.Common.Notifications.Impl
{
    public interface INotificationMessageDispatcher
    {
        Task Dispatch(NotificationType notificationType, NotificationMessage message, RecipientContactInfo recipient);
    }
}