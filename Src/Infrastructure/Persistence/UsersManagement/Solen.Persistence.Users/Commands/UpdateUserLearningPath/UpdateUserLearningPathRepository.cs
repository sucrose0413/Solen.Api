using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Users.Commands
{
    public class UpdateUserLearningPathRepository : IUpdateUserLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public UpdateUserLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token)
        {
            return await _context.LearningPaths.FirstOrDefaultAsync(x => x.Id == learningPathId, token);
        }
    }
}