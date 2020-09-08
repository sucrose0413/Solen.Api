using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Settings.Organization.Queries
{
    public class GetOrganizationInfoQueryHandler : IRequestHandler<GetOrganizationInfoQuery, OrganizationInfoViewModel>
    {
        private readonly IGetOrganizationInfoService _service;

        public GetOrganizationInfoQueryHandler(IGetOrganizationInfoService service)
        {
            _service = service;
        }

        public async Task<OrganizationInfoViewModel> Handle(GetOrganizationInfoQuery query, CancellationToken token)
        {
            var subscriptionPlan = await _service.GetOrganizationSubscriptionPlan(token);
            var organizationName = await _service.GetOrganizationName(token);
            var currentStorage = await _service.GetCurrentStorage(token);
            var currentUsersCount = await _service.GetCurrentUserCount(token);

            return new OrganizationInfoViewModel
            {
                OrganizationName = organizationName,
                SubscriptionPlan = subscriptionPlan.Name,
                MaxStorage = subscriptionPlan.MaxStorage,
                CurrentStorage = currentStorage,
                CurrentUsersCount = currentUsersCount
            };
        }
    }
}