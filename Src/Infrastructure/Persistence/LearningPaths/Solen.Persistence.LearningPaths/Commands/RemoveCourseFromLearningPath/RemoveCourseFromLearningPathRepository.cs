using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class RemoveCourseFromLearningPathRepository : IRemoveCourseFromLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public RemoveCourseFromLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearningPathCourse> GetLearningPathCourse(string learningPathId, string courseId,
            CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.LearningPathId == learningPathId && x.CourseId == courseId)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public void RemoveLearningPathCourse(LearningPathCourse learningPathCourse)
        {
            _context.LearningPathCourses.Remove(learningPathCourse);
        }
    }
}