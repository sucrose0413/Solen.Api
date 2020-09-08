using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Solen.Core.Application.Settings.Notifications.Services.Queries
{
    public interface IGetNotificationTemplatesRepository
    {
        Task<IEnumerable<string>> GetDisabledNotifications(string organizationId, CancellationToken token);
        Task<IList<NotificationTemplateDto>> GetNotificationTemplates(IEnumerable<string> disabledNotifications, CancellationToken token);
    }
}