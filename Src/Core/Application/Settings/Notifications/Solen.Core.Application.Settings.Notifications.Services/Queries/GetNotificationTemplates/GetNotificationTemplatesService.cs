using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Solen.Core.Application.Settings.Notifications.Services.Queries
{
    public class GetNotificationTemplatesService : IGetNotificationTemplatesService
    {
        private readonly IGetNotificationTemplatesRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetNotificationTemplatesService(IGetNotificationTemplatesRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<NotificationTemplateDto>> GetNotificationTemplates(CancellationToken token)
        {
            var disabledNotifications =
                await _repo.GetDisabledNotifications(_currentUserAccessor.OrganizationId, token);

            return await _repo.GetNotificationTemplates(disabledNotifications, token);
        }
    }
}