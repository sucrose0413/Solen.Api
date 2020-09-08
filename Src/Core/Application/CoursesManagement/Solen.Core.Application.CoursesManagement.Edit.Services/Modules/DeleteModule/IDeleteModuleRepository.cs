using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public interface IDeleteModuleRepository
    {
        void RemoveModule(Module module);
    }
}