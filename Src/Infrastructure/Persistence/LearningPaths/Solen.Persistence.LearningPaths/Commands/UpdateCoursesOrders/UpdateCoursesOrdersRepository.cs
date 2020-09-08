using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class UpdateCoursesOrdersRepository : IUpdateCoursesOrdersRepository
    {
        private readonly SolenDbContext _context;

        public UpdateCoursesOrdersRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<List<LearningPathCourse>> GetLearningPathCourses(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.LearningPathId == learningPathId && x.LearningPath.OrganizationId == organizationId)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public void UpdateLearningPathCourses(IEnumerable<LearningPathCourse> courses)
        {
            _context.LearningPathCourses.UpdateRange(courses);
        }
    }
}