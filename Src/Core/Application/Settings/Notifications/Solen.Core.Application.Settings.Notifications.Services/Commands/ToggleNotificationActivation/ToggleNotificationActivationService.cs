using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Notifications.Commands;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Settings.Notifications.Services.Commands
{
    public class ToggleNotificationActivationService : IToggleNotificationActivationService
    {
        private readonly IToggleNotificationActivationRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public ToggleNotificationActivationService(IToggleNotificationActivationRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task ActivateNotificationTemplate(string templateId, CancellationToken token)
        {
            var disabledTemplate =
                await _repo.GetDisabledNotification(_currentUserAccessor.OrganizationId, templateId, token);

            if (disabledTemplate != null)
                _repo.RemoveDisabledNotification(disabledTemplate);
        }

        public async Task DeactivateNotificationTemplate(string templateId, CancellationToken token)
        {
            if (await _repo.GetDisabledNotification(_currentUserAccessor.OrganizationId, templateId, token) == null)
                _repo.AddDisabledNotification(new DisabledNotificationTemplate(_currentUserAccessor.OrganizationId,
                    templateId));
        }
    }
}