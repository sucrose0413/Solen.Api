using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace Solen.Persistence.LearningPaths.Queries
{
    public class GetLearnerProgressRepository : IGetLearnerProgressRepository
    {
        private readonly SolenDbContext _context;

        public GetLearnerProgressRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<LearningPathCourseDto>> GetLearningPathPublishedCourses(string learningPthId,
            string publishedStatus, CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.LearningPathId == learningPthId
                            && x.Course.CourseStatusName == publishedStatus)
                .Select(x => new LearningPathCourseDto(
                    x.CourseId,
                    x.Course.Title,
                    x.Course.Modules.SelectMany(l => l.Lectures).Count()
                ))
                .ToListAsync(token);
        }

        public async Task<int> GetLearnerCompletedLectures(string learnerId, string courseId, string publishedStatus,
            CancellationToken token)
        {
            return await _context.LearnerCompletedLectures
                .Where(x => x.LearnerId == learnerId && x.Lecture.Module.CourseId == courseId &&
                            x.Lecture.Module.Course.CourseStatusName == publishedStatus)
                .CountAsync(token);
        }
    }
}