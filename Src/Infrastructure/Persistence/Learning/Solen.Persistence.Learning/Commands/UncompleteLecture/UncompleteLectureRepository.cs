using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Learning.Commands
{
    public class UncompleteLectureRepository : IUncompleteLectureRepository
    {
        private readonly SolenDbContext _context;

        public UncompleteLectureRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearnerCompletedLecture> GetCompletedLecture(string learnerId, string lectureId,
            CancellationToken token)
        {
            return await _context.LearnerCompletedLectures
                .FirstOrDefaultAsync(x => x.LearnerId == learnerId && x.LectureId == lectureId, token);
        }

        public void RemoveLearnerCompletedLecture(LearnerCompletedLecture lecture)
        {
            _context.LearnerCompletedLectures.Remove(lecture);
        }
    }
}