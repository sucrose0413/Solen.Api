using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.UserProfile.Queries
{
    public interface IGetCurrentUserInfoService
    {
        Task<UserForProfileDto> GetCurrentUserInfo(CancellationToken token);
    }
}