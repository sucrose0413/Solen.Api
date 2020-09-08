using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public interface IUpdateModuleService
    {
        void UpdateModuleName(Module module, string name);
        void UpdateModuleRepo(Module module);
    }
}