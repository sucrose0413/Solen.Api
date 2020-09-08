using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public interface IGetNotificationTemplateService
    {
        Task<NotificationTemplateDto> GetNotificationTemplate(string templateId, CancellationToken token);
    }
}