using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.Common.Subscription.Impl
{
    public interface IOrganizationSubscriptionRepository
    {
        Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(string organizationId, CancellationToken token);
        Task<long> GetOrganizationCurrentStorage(string organizationId, CancellationToken token);
        Task<int> GetOrganizationCurrentUsersCount(string organizationId, CancellationToken token);
    }
}