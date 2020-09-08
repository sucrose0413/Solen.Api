using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Users.Commands
{
    public class InviteMembersRepository : IInviteMembersRepository
    {
        private readonly SolenDbContext _context;

        public InviteMembersRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearningPath> GetLearningPath(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.Id == learningPathId && x.OrganizationId == organizationId)
                .FirstOrDefaultAsync(token);
        }
    }
}
