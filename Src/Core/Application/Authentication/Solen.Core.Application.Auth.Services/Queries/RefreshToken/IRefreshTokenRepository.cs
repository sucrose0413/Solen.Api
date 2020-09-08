using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshToken(string refreshToken, CancellationToken cancellationToken);
        Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}