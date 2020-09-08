using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Users.Queries;
using Solen.Core.Application.Users.Services.Queries;

namespace Solen.Persistence.Users.Queries
{
    public class GetUserRepository : IGetUserRepository
    {
        private readonly SolenDbContext _context;

        public GetUserRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<LearningPathForUserDto>> GetLearningPaths(string organizationId, CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => new LearningPathForUserDto(x.Id, x.Name))
                .ToListAsync(token);
        }
    }
}