using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Notifications.Queries;
using Solen.Core.Application.Notifications.Services.Queries;

namespace Solen.Persistence.Notifications.Queries
{
    public class GetNotificationsRepository : IGetNotificationsRepository
    {
        private readonly SolenDbContext _context;

        public GetNotificationsRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<NotificationDto>> GetNotifications(string recipientId, CancellationToken token)
        {
            return await _context.Notifications
                .Where(x => x.RecipientId == recipientId)
                .OrderByDescending(x => x.CreationDate)
                .Select(x =>
                    new NotificationDto(x.Id, x.NotificationEvent.Name, x.Subject, x.Body, x.CreationDate, x.IsRead))
                .ToListAsync(token);
        }
    }
}