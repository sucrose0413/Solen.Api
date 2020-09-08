using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Notifications.Commands
{
    public interface IMarkNotificationAsReadService
    {
        Task<Notification> GetNotification(string notificationId, CancellationToken token);
        void MarkNotificationAsRead(Notification notification);
        void UpdateNotificationRepo(Notification notification);
    }
}