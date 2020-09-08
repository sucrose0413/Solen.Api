using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Notifications.Queries
{
    public interface IGetNotificationsService
    {
        Task<IList<NotificationDto>> GetNotifications(CancellationToken token);
    }
}