using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Learning.Commands
{
    public class CompleteLectureRepository : ICompleteLectureRepository
    {
        private readonly SolenDbContext _context;

        public CompleteLectureRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesLectureExist(string lectureId, string learningPathId, CancellationToken token)
        {
            return await _context.Lectures
                .AnyAsync(x => x.Id == lectureId &&
                               x.Module.Course.CourseLearningPaths.Any(c => c.LearningPathId == learningPathId), token);
        }

        public async Task<bool> IsTheLectureAlreadyCompleted(string lectureId, string learnerId,
            CancellationToken token)
        {
            return await _context.LearnerCompletedLectures
                .AnyAsync(x => x.LearnerId == learnerId && x.LectureId == lectureId,
                    token);
        }

        public void AddLearnerCompletedLecture(LearnerCompletedLecture lecture)
        {
            _context.LearnerCompletedLectures.Add(lecture);
        }
    }
}