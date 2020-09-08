using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Dashboard.Queries;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public class GetUserCountInfoService : IGetUserCountInfoService
    {
        private readonly IOrganizationSubscriptionManager _subscriptionManager;

        public GetUserCountInfoService(IOrganizationSubscriptionManager subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        public async Task<UserCountInfoDto> GetUserCountInfo(CancellationToken token)
        {
            var subscription = await _subscriptionManager.GetOrganizationSubscriptionPlan(token);
            var currentUserCount = await _subscriptionManager.GetOrganizationCurrentUserCount(token);

            return new UserCountInfoDto
            {
                MaximumUsers = subscription.MaxUsers,
                CurrentUserCount = currentUserCount
            };
        }
    }
}