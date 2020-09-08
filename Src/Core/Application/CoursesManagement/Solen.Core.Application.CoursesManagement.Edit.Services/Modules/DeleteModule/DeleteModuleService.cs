using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public class DeleteModuleService : IDeleteModuleService
    {
        private readonly IDeleteModuleRepository _repo;

        public DeleteModuleService(IDeleteModuleRepository repo)
        {
            _repo = repo;
        }
        
        public void RemoveModuleFromRepo(Module module)
        {
            _repo.RemoveModule(module);
        }
    }
}