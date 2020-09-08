using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public interface ICreateModuleService
    {
        Task ControlCourseExistenceAndStatus(string courseId, CancellationToken token);
        Module CreateModule(CreateModuleCommand command);
        Task AddModuleToRepo(Module module, CancellationToken token);
    }
}