using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public interface IModulesCommonRepository
    {
        Task<Module> GetModuleWithCourse(string moduleId, string organizationId, CancellationToken token);
    }
}