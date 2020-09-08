using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Dashboard.Queries
{
    public interface IGetStorageInfoService
    {
        Task<StorageInfoDto> GetStorageInfo(CancellationToken token);
    }
}