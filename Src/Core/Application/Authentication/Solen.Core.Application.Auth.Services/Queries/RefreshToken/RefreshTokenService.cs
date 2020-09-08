using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;
        private readonly IDateTime _dateTime;

        public RefreshTokenService(IRefreshTokenRepository repo, IDateTime dateTime)
        {
            _repo = repo;
            _dateTime = dateTime;
        }

        public async Task<RefreshToken> GetCurrentRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            return await _repo.GetRefreshToken(refreshToken, cancellationToken) ??
                   throw new UnauthorizedException("Invalid refresh token");
        }

        public void CheckRefreshTokenValidity(RefreshToken refreshToken)
        {
            if (refreshToken.ExpiryTime.HasValue && refreshToken.ExpiryTime < _dateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired");
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            return await _repo.GetUserByRefreshToken(refreshToken, cancellationToken);
        }
    }
}