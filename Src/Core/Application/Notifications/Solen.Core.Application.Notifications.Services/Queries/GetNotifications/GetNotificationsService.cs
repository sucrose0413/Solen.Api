using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Notifications.Queries;

namespace Solen.Core.Application.Notifications.Services.Queries
{
    public class GetNotificationsService : IGetNotificationsService
    {
        private readonly IGetNotificationsRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetNotificationsService(IGetNotificationsRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<NotificationDto>> GetNotifications(CancellationToken token)
        {
            return await _repo.GetNotifications(_currentUserAccessor.UserId, token);
        }
    }
}