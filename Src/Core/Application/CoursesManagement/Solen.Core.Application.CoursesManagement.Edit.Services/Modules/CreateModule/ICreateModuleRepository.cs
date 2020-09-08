using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public interface ICreateModuleRepository
    {
        Task<Course> GetCourse(string courseId, string organizationId, CancellationToken token);
        Task AddModule(Module module, CancellationToken token);
    }
}