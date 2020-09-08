using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Modules
{
    public class UpdateModuleRepository : IUpdateModuleRepository
    {
        private readonly SolenDbContext _context;

        public UpdateModuleRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public void UpdateModule(Module module)
        {
            _context.Modules.Update(module);
        }
    }
}