using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Lectures
{
    public class CreateLectureRepository : ICreateLectureRepository
    {
        private readonly SolenDbContext _context;

        public CreateLectureRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task AddLecture(Lecture lecture, CancellationToken token)
        {
            await _context.Lectures.AddAsync(lecture, token);
        }

        public async Task<Module> GetModuleWithCourse(string moduleId, string organizationId, CancellationToken token)
        {
            return await _context.Modules
                .Include(x => x.Course)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == moduleId
                                           && x.Course.Creator.OrganizationId == organizationId, token);
        }
    }
}