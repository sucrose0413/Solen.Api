using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Solen.Core.Application.Settings.Notifications.Services.Queries
{
    public class GetNotificationTemplateService : IGetNotificationTemplateService
    {
        private readonly IGetNotificationTemplateRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetNotificationTemplateService(IGetNotificationTemplateRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<NotificationTemplateDto> GetNotificationTemplate(string templateId, CancellationToken token)
        {
            var disabledNotifications =
                await _repo.GetDisabledNotifications(_currentUserAccessor.OrganizationId, token);

            return await _repo.GetNotificationTemplate(templateId, disabledNotifications, token) ??
                   throw new NotFoundException($"The Notification Template ({templateId}) does not exist");
        }
    }
}