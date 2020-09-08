using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Core.Application.Settings.Notifications.Services.Commands
{
    public interface IToggleNotificationActivationRepository
    {
        Task<DisabledNotificationTemplate> GetDisabledNotification(string organizationId, string templateId,
            CancellationToken token);
        void AddDisabledNotification(DisabledNotificationTemplate disabledNotification);
        void RemoveDisabledNotification(DisabledNotificationTemplate disabledNotification);
    }
}