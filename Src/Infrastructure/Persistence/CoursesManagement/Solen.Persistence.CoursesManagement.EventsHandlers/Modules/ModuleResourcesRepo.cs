using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Modules;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Persistence.CoursesManagement.EventsHandlers.Modules
{
    public class ModuleResourcesRepo : IModuleResourcesRepo
    {
        private readonly SolenDbContext _context;

        public ModuleResourcesRepo(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<AppResource>> GetModuleResources(string moduleId, CancellationToken token)
        {
            return await _context.CourseResources
                .Where(x => x.ModuleId == moduleId)
                .Select(x => x.Resource)
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}