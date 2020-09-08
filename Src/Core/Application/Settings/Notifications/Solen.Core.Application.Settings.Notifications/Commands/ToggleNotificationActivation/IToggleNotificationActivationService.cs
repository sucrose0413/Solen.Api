using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Settings.Notifications.Commands
{
    public interface IToggleNotificationActivationService
    {
        Task ActivateNotificationTemplate(string templateId, CancellationToken token);
        Task DeactivateNotificationTemplate(string templateId, CancellationToken token);
    }
}