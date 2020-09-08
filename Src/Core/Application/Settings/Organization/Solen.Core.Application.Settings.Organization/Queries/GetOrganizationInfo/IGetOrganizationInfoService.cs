using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.Settings.Organization.Queries
{
    public interface IGetOrganizationInfoService
    {
        Task<string> GetOrganizationName(CancellationToken token);
        Task<SubscriptionPlan> GetOrganizationSubscriptionPlan(CancellationToken token);
        Task<long> GetCurrentStorage(CancellationToken token);
        Task<int> GetCurrentUserCount(CancellationToken token);
    }
}