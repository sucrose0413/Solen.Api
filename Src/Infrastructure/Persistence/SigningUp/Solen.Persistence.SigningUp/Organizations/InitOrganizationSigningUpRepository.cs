using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.SigningUp.Services.Organizations;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence.SigningUp.Organizations
{
    public class InitOrganizationSigningUpRepository : IInitOrganizationSigningUpRepository
    {
        private readonly SolenDbContext _context;

        public InitOrganizationSigningUpRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task AddOrganizationSigningUp(OrganizationSigningUp signingUp, CancellationToken token)
        {
            await _context.OrganizationSigningUps.AddAsync(signingUp, token);
        }
    }
}