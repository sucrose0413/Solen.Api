using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.Common.Subscription.Impl
{
    public class OrganizationSubscriptionManager : IOrganizationSubscriptionManager
    {
        private readonly IOrganizationSubscriptionRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public OrganizationSubscriptionManager(IOrganizationSubscriptionRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(CancellationToken token)
        {
            return await _repo.GetOrganizationSubscriptionPlan(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<long> GetOrganizationCurrentStorage(CancellationToken token)
        {
            return await _repo.GetOrganizationCurrentStorage(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<int> GetOrganizationCurrentUserCount(CancellationToken token)
        {
            return await _repo.GetOrganizationCurrentUsersCount(_currentUserAccessor.OrganizationId, token);
        }
    }
}