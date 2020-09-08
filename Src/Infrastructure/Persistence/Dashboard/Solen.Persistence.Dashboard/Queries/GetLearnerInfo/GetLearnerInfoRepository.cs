using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Dashboard.Queries
{
    public class GetLearnerInfoRepository : IGetLearnerInfoRepository
    {
        private readonly SolenDbContext _context;

        public GetLearnerInfoRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetLastAccessedCourse(string learnerId, CancellationToken token)
        {
            return await _context.LearnerCourseAccessTimes
                .Where(x => x.LearnerId == learnerId)
                .OrderByDescending(x => x.AccessDate)
                .Select(x => x.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public async Task<Course> GetLearningPathFirstCourse(string learningPathId, CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.LearningPathId == learningPathId)
                .OrderBy(x => x.Order)
                .Select(x => x.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public async Task<int> GetCourseTotalDuration(string courseId, CancellationToken token)
        {
            return await _context.Courses
                .Where(x => x.Id == courseId)
                .Select(x => x.Modules.SelectMany(l => l.Lectures).Sum(l => l.Duration))
                .FirstOrDefaultAsync(token);
        }

        public async Task<int> GetLearnerCompletedDuration(string learnerId, string courseId, CancellationToken token)
        {
            return await _context.LearnerCompletedLectures
                .Where(x => x.LearnerId == learnerId)
                .SumAsync(x => x.Lecture.Duration, token);
        }
    }
}