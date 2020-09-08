using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Persistence.Auth.Queries
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SolenDbContext _context;

        public RefreshTokenRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == refreshToken, cancellationToken);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Where(x => x.Id == refreshToken)
                .Include(x => x.User.UserRoles)
                .Select(x => x.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}