using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.SigningUp.Services.Organizations.Queries;

namespace Solen.Persistence.SigningUp.Organizations.Queries
{
    public class CheckOrganizationSigningUpTokenRepository : ICheckOrganizationSigningUpTokenRepository
    {
        private readonly SolenDbContext _context;

        public CheckOrganizationSigningUpTokenRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesSigningUpTokenExist(string signingUpToken, CancellationToken token)
        {
            return await _context.OrganizationSigningUps
                .AnyAsync(x => x.Token == signingUpToken, token);
        }
    }
}