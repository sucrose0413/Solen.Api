using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Persistence.Auth.Queries
{
    public class CommonRepository : ICommonRepository
    {
        private readonly SolenDbContext _context;

        public CommonRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task RemoveAnyUserRefreshToken(string userId, CancellationToken token)
        {
            var refreshTokens = await _context.RefreshTokens.Where(x => x.UserId == userId)
                .ToListAsync(token);

            _context.RefreshTokens.RemoveRange(refreshTokens);
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
        }
    }
}