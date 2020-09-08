using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.SigningUp.Services.Users.Queries;

namespace Solen.Persistence.SigningUp.Users.Queries
{
    public class CheckUserSigningUpTokenRepository : ICheckUserSigningUpTokenRepository
    {
        private readonly SolenDbContext _context;

        public CheckUserSigningUpTokenRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesSigningUpTokenExist(string signingUpToken, CancellationToken token)
        {
            return await _context.Users
                .AnyAsync(x => x.InvitationToken == signingUpToken, token);
        }
    }
}