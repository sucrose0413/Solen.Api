using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public interface IModulesCommonService
    {
        Task<Module> GetModuleFromRepo(string moduleId, CancellationToken token);
        void CheckCourseStatusForModification(Module module);
    }
}