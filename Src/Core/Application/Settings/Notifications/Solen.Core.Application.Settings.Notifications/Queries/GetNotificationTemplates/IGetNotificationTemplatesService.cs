using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public interface IGetNotificationTemplatesService
    {
        Task<IList<NotificationTemplateDto>> GetNotificationTemplates(CancellationToken token);
    }
}