using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Settings.Organization.Services.Commands;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Solen.Persistence.Settings.Organization.Commands
{
    public class UpdateOrganizationInfoRepository : IUpdateOrganizationInfoRepository
    {
        private readonly SolenDbContext _context;

        public UpdateOrganizationInfoRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Org> GetOrganization(string organizationId, CancellationToken token)
        {
            return await _context.Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == organizationId, token);
        }

        public void UpdateOrganization(Org organization)
        {
            _context.Organizations.Update(organization);
        }
    }
}