using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class UpdateLearningPathRepository : IUpdateLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public UpdateLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<LearningPath> GetLearningPath(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.Id == learningPathId && x.OrganizationId == organizationId)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public void UpdateLearningPath(LearningPath learningPath)
        {
            _context.LearningPaths.Update(learningPath);
        }
    }
}