using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class DeleteLearningPathRepository : IDeleteLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public DeleteLearningPathRepository(SolenDbContext context)
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

        public void RemoveLearningPath(LearningPath learningPath)
        {
            _context.LearningPaths.Remove(learningPath);
        }

        public async Task<List<User>> GetLearningPathUsers(string learningPathId, CancellationToken token)
        {
            return await _context.Users
                .Where(x => x.LearningPathId == learningPathId)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<LearningPath> GetLearningPathByName(string learningPathName, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .FirstOrDefaultAsync(x => x.Name == learningPathName
                                          && x.OrganizationId == organizationId, token);
        }
    }
}