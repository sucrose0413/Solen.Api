using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class UpdateModulesOrdersRepository : IUpdateModulesOrdersRepository
    {
        private readonly SolenDbContext _context;

        public UpdateModulesOrdersRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<List<Module>> GetCourseModules(string courseId, CancellationToken token)
        {
            return await _context.Modules
                .Where(m => m.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public void UpdateModules(IEnumerable<Module> modules)
        {
            _context.Modules.UpdateRange(modules);
        }
    }
}