using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class AddCoursesToLearningPathRepository : IAddCoursesToLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public AddCoursesToLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearningPath> GetLearningPathWithCourses(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.Id == learningPathId && x.OrganizationId == organizationId)
                .Include(x => x.LearningPathCourses)
                .FirstOrDefaultAsync(token);
        }

        public async Task<bool> DoesCourseExist(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .AnyAsync(x => x.Id == courseId && x.Creator.OrganizationId == organizationId, token);
        }

        public void UpdateLearningPath(LearningPath learningPath)
        {
            _context.LearningPaths.Update(learningPath);
        }
    }
}