using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Notifications.Queries;

namespace Solen.Core.Application.Notifications.Services.Queries
{
    public interface IGetNotificationsRepository
    {
        Task<IList<NotificationDto>> GetNotifications(string recipientId, CancellationToken token);
    }
}