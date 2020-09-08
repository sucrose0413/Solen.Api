using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.LearningPaths;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.LearningPaths
{
    public class UpdateCourseLearningPathsRepository : IUpdateCourseLearningPathsRepository
    {
        private readonly SolenDbContext _context;

        public UpdateCourseLearningPathsRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<Course> GetCourseWithLearningPaths(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .Include(c => c.CourseLearningPaths)
                .SingleOrDefaultAsync(c => c.Id == courseId && c.Creator.OrganizationId == organizationId, token);
        }

        public async Task<int?> GetLearningPathLastOrder(string learningPathId, CancellationToken token)
        {
            var maxOrder = await _context.LearningPathCourses
                .Where(l => l.LearningPathId == learningPathId)
                .MaxAsync(l => (int?) l.Order, token) ?? 0;

            return maxOrder;
        }

        public async Task<bool> DoesLearningPathExist(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths.AnyAsync(
                x => x.Id == learningPathId && x.OrganizationId == organizationId, token);
        }
        
        public void UpdateCourseLearningPaths(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}