using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public interface IDeleteModuleService
    {
        void RemoveModuleFromRepo(Module module);
    }
}