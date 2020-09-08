using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Modules
{
    public class CreateModuleRepository : ICreateModuleRepository
    {
        private readonly SolenDbContext _context;

        public CreateModuleRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetCourse(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == courseId && x.Creator.OrganizationId == organizationId, token);
        }

        public async Task AddModule(Module module, CancellationToken token)
        {
            await _context.Modules.AddAsync(module, token);
        }
    }
}