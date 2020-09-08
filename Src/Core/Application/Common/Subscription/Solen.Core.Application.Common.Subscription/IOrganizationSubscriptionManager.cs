using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.Common.Subscription
{
    public interface IOrganizationSubscriptionManager
    {
        Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(CancellationToken token);
        Task<long> GetOrganizationCurrentStorage(CancellationToken token);
        Task<int> GetOrganizationCurrentUserCount(CancellationToken token);
    }
}
