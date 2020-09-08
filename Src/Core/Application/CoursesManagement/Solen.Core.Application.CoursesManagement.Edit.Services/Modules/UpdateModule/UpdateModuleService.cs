using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public class UpdateModuleService : IUpdateModuleService
    {
        private readonly IUpdateModuleRepository _repo;

        public UpdateModuleService(IUpdateModuleRepository repo)
        {
            _repo = repo;
        }

        public void UpdateModuleName(Module module, string name)
        {
            module.UpdateName(name);
        }

        public void UpdateModuleRepo(Module module)
        {
            _repo.UpdateModule(module);
        }
    }
}