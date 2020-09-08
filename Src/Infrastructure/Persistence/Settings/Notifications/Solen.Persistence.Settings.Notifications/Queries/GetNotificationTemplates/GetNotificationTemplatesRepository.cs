using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.Core.Application.Settings.Notifications.Services.Queries;

namespace Solen.Persistence.Settings.Notifications.Queries
{
    public class GetNotificationTemplatesRepository : IGetNotificationTemplatesRepository
    {
        private readonly SolenDbContext _context;

        public GetNotificationTemplatesRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetDisabledNotifications(string organizationId, CancellationToken token)
        {
            return await _context.DisabledNotificationTemplates
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => x.NotificationTemplateId).ToListAsync(token);
        }

        public async Task<IList<NotificationTemplateDto>> GetNotificationTemplates(
            IEnumerable<string> disabledNotifications, CancellationToken token)
        {
            return await _context.NotificationTemplates
                .Where(x => !x.IsSystemNotification)
                .Select(x => new NotificationTemplateDto
                {
                    Id = x.Id,
                    Type = x.TypeName,
                    Event = x.NotificationEvent.Description,
                    IsActivated = disabledNotifications.All(id => id != x.Id)
                })
                .ToListAsync(token);
        }
    }
}