using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Modules
{
    public class DeleteModuleRepository : IDeleteModuleRepository
    {
        private readonly SolenDbContext _context;

        public DeleteModuleRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public void RemoveModule(Module module)
        {
            _context.Modules.Remove(module);
        }
    }
}