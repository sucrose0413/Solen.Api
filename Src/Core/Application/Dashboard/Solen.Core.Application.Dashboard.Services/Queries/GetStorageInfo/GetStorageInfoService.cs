using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Dashboard.Queries;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public class GetStorageInfoService : IGetStorageInfoService
    {
        private readonly IOrganizationSubscriptionManager _subscriptionManager;

        public GetStorageInfoService(IOrganizationSubscriptionManager subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        public async Task<StorageInfoDto> GetStorageInfo(CancellationToken token)
        {
            var subscription = await _subscriptionManager.GetOrganizationSubscriptionPlan(token);
            var currentStorage = await _subscriptionManager.GetOrganizationCurrentStorage(token);
            
            return new StorageInfoDto
            {
                MaximumStorage = subscription.MaxStorage,
                CurrentStorage = currentStorage
            };
        }
    }
}