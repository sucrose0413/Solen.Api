using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Queries
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetCurrentRefreshToken(string refreshToken, CancellationToken cancellationToken);
        void CheckRefreshTokenValidity(RefreshToken refreshToken);
        Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}