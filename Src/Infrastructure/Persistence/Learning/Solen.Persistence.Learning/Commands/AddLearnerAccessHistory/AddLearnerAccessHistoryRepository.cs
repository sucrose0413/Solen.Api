using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Learning.Commands
{
    public class AddLearnerAccessHistoryRepository : IAddLearnerAccessHistoryRepository
    {
        private readonly SolenDbContext _context;

        public AddLearnerAccessHistoryRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetLectureCourseId(string lectureId, string learningPathId, CancellationToken token)
        {
            return await _context.Lectures
                .Where(x => x.Id == lectureId &&
                            x.Module.Course.CourseLearningPaths.Any(c => c.LearningPathId == learningPathId))
                .Select(x => x.Module.CourseId).FirstOrDefaultAsync(token);
        }

        public void AddLearnerLectureAccessHistory(LearnerLectureAccessHistory history)
        {
            _context.LearnerLectureAccessHistories.Add(history);
        }

        public void AddLearnerCourseAccessTime(LearnerCourseAccessTime access)
        {
            _context.LearnerCourseAccessTimes.Add(access);
        }

        public void UpdateLearnerCourseAccessTime(LearnerCourseAccessTime access)
        {
            _context.LearnerCourseAccessTimes.Update(access);
        }

        public async Task<LearnerCourseAccessTime> GetLearnerCourseAccessTime(string learnerId, string courseId, CancellationToken token)
        {
            return await _context.LearnerCourseAccessTimes
                .Where(x => x.LearnerId == learnerId && x.CourseId == courseId)
                .FirstOrDefaultAsync(token);
        }
    }
}