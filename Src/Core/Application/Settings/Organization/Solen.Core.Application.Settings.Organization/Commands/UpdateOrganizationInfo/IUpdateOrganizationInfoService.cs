using System.Threading;
using System.Threading.Tasks;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Solen.Core.Application.Settings.Organization.Commands
{
    public interface IUpdateOrganizationInfoService
    {
        Task<Org> GetOrganization(CancellationToken token);
        void UpdateOrganizationName(Org organization, string name);
        void UpdateOrganizationRepo(Org organization);
    }
}