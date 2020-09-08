using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Settings.Organization.Services.Queries;

namespace Solen.Persistence.Settings.Organization.Queries
{
    public class GetOrganizationInfoRepository : IGetOrganizationInfoRepository
    {
        private readonly SolenDbContext _context;

        public GetOrganizationInfoRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetOrganizationName(string organizationId, CancellationToken token)
        {
            return await _context.Organizations.Where(x => x.Id == organizationId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync(token);
        }
    }
}