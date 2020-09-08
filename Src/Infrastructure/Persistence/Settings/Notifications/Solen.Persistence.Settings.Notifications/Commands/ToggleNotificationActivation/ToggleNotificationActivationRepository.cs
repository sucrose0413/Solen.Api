using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Settings.Notifications.Services.Commands;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Persistence.Settings.Notifications.Commands
{
    public class ToggleNotificationActivationRepository : IToggleNotificationActivationRepository
    {
        private readonly SolenDbContext _context;

        public ToggleNotificationActivationRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<DisabledNotificationTemplate> GetDisabledNotification(string organizationId,
            string templateId, CancellationToken token)
        {
            return await _context.DisabledNotificationTemplates.FirstOrDefaultAsync(x =>
                x.OrganizationId == organizationId && x.NotificationTemplateId == templateId, token);
        }

        public void AddDisabledNotification(DisabledNotificationTemplate disabledNotification)
        {
            _context.DisabledNotificationTemplates.Add(disabledNotification);
        }

        public void RemoveDisabledNotification(DisabledNotificationTemplate disabledNotification)
        {
            _context.DisabledNotificationTemplates.Remove(disabledNotification);
        }
    }
}