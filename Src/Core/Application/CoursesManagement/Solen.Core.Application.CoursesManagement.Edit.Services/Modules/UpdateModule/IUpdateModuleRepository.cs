using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public interface IUpdateModuleRepository
    {
        void UpdateModule(Module module);
    }
}