using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Services.Queries;

namespace Solen.Persistence.Learning.Queries
{
    public class GetCompletedLecturesRepository : IGetCompletedLecturesRepository
    {
        private readonly SolenDbContext _context;

        public GetCompletedLecturesRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<string>> GetLearnerCompletedLectures(string courseId, string learnerId,
            CancellationToken token)
        {
            return await _context.LearnerCompletedLectures
                .Where(x => x.LearnerId == learnerId && x.Lecture.Module.CourseId == courseId)
                .Select(x => x.LectureId)
                .ToListAsync(token);
        }
    }
}