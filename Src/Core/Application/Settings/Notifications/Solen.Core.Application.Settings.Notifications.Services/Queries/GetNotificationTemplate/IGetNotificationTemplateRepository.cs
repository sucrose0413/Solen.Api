using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Solen.Core.Application.Settings.Notifications.Services.Queries
{
    public interface IGetNotificationTemplateRepository
    {
        Task<IEnumerable<string>> GetDisabledNotifications(string organizationId, CancellationToken token);

        Task<NotificationTemplateDto> GetNotificationTemplate(string templateId,
            IEnumerable<string> disabledNotifications, CancellationToken token);
    }
}