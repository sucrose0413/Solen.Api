using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Lectures;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Persistence.CoursesManagement.EventsHandlers.Lectures
{
    public class LectureResourcesRepo : ILectureResourcesRepo
    {
        private readonly SolenDbContext _context;

        public LectureResourcesRepo(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<AppResource>> GetLectureResources(string lectureId, CancellationToken token)
        {
            return await _context.CourseResources
                .Where(x => x.LectureId == lectureId)
                .Select(x => x.Resource)
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}
