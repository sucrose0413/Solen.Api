using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Dashboard.Queries
{
    public interface IGetUserCountInfoService
    {
        Task<UserCountInfoDto> GetUserCountInfo(CancellationToken token);
    }
}