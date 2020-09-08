using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Settings.Organization.Services.Queries
{
    public interface IGetOrganizationInfoRepository
    {
        Task<string> GetOrganizationName(string organizationId, CancellationToken token);
    }
}