using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Notifications.Services.Commands;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Persistence.Notifications.Commands
{
    public class MarkNotificationAsReadRepository : IMarkNotificationAsReadRepository
    {
        private readonly SolenDbContext _context;

        public MarkNotificationAsReadRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> GetNotificationById(string notificationId, CancellationToken token)
        {
            return await _context
                .Notifications
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == notificationId, token);
        }

        public void UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
        }
    }
}