using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Lectures
{
    public class LecturesCommonRepository : ILecturesCommonRepository
    {
        private readonly SolenDbContext _context;

        public LecturesCommonRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Lecture> GetLectureWithCourse(string lectureId, string organizationId,
            CancellationToken token)
        {
            return await _context.Lectures
                .Include(x => x.Module)
                .ThenInclude(m => m.Course)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == lectureId
                                           && x.Module.Course.Creator.OrganizationId == organizationId, token);
        }
    }
}