using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Subscription.Impl;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence.Common.Subscription
{
    public class OrganizationSubscriptionRepository : IOrganizationSubscriptionRepository
    {
        private readonly SolenDbContext _context;

        public OrganizationSubscriptionRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(string organizationId,
            CancellationToken token)
        {
            return await _context.Organizations
                .Where(x => x.Id == organizationId)
                .Select(x => x.SubscriptionPlan)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public async Task<long> GetOrganizationCurrentStorage(string organizationId, CancellationToken token)
        {
            return await _context.AppResources
                .Where(x => x.OrganizationId == organizationId && !x.ToDelete)
                .SumAsync(x => x.Size, token);
        }

        public async Task<int> GetOrganizationCurrentUsersCount(string organizationId, CancellationToken token)
        {
            return await _context.Users.CountAsync(x => x.OrganizationId == organizationId, token);
        }
    }
}