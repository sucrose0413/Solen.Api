using System.Threading;
using System.Threading.Tasks;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Solen.Core.Application.Settings.Organization.Services.Commands
{
    public interface IUpdateOrganizationInfoRepository
    {
        Task<Org> GetOrganization(string organizationId, CancellationToken token);
        void UpdateOrganization(Org organization);
    }
}