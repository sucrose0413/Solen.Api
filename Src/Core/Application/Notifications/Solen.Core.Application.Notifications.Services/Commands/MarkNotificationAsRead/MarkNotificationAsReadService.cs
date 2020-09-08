using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Notifications.Commands;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Notifications.Services.Commands
{
    public class MarkNotificationAsReadService : IMarkNotificationAsReadService
    {
        private readonly IMarkNotificationAsReadRepository _repo;

        public MarkNotificationAsReadService(IMarkNotificationAsReadRepository repo)
        {
            _repo = repo;
        }

        public async Task<Notification> GetNotification(string notificationId, CancellationToken token)
        {
            return await _repo.GetNotificationById(notificationId, token);
        }

        public void MarkNotificationAsRead(Notification notification)
        {
            notification.MarkAsRead();
        }

        public void UpdateNotificationRepo(Notification notification)
        {
            _repo.UpdateNotification(notification);
        }
    }
}