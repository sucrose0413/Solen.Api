using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Settings.Organization.Queries;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.Settings.Organization.Services.Queries
{
    public class GetOrganizationInfoService : IGetOrganizationInfoService
    {
        private readonly IGetOrganizationInfoRepository _repo;
        private readonly IOrganizationSubscriptionManager _subscriptionManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetOrganizationInfoService(IGetOrganizationInfoRepository repo,
            IOrganizationSubscriptionManager subscriptionManager, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _subscriptionManager = subscriptionManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<string> GetOrganizationName(CancellationToken token)
        {
            return await _repo.GetOrganizationName(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(CancellationToken token)
        {
            return await _subscriptionManager.GetOrganizationSubscriptionPlan(token);
        }

        public async Task<long> GetCurrentStorage(CancellationToken token)
        {
            return await _subscriptionManager.GetOrganizationCurrentStorage(token);
        }

        public async Task<int> GetCurrentUserCount(CancellationToken token)
        {
            return await _subscriptionManager.GetOrganizationCurrentUserCount(token);
        }
    }
}