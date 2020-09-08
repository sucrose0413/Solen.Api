using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Settings.Organization.Commands;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Solen.Core.Application.Settings.Organization.Services.Commands
{
    public class UpdateOrganizationInfoService : IUpdateOrganizationInfoService
    {
        private readonly IUpdateOrganizationInfoRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateOrganizationInfoService(IUpdateOrganizationInfoRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Org> GetOrganization(CancellationToken token)
        {
            return await _repo.GetOrganization(_currentUserAccessor.OrganizationId, token);
        }

        public void UpdateOrganizationName(Org organization, string name)
        {
            organization.UpdateName(name);
        }

        public void UpdateOrganizationRepo(Org organization)
        {
            _repo.UpdateOrganization(organization);
        }
    }
}