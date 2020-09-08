using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Modules
{
    public class ModulesCommonRepository : IModulesCommonRepository
    {
        private readonly SolenDbContext _context;

        public ModulesCommonRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<Module> GetModuleWithCourse(string moduleId, string organizationId, CancellationToken token)
        {
            return await _context.Modules
                .Include(x => x.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == moduleId
                                          && x.Course.Creator.OrganizationId == organizationId, token);
        }
    }
}