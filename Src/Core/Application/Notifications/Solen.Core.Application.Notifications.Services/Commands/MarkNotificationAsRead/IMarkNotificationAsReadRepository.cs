using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Notifications.Services.Commands
{
    public interface IMarkNotificationAsReadRepository
    {
        Task<Notification> GetNotificationById(string notificationId, CancellationToken token);
        void UpdateNotification(Notification notification);
    }
}