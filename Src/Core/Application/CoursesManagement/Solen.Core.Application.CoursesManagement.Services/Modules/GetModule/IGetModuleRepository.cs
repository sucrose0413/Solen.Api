using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Modules
{
    public interface IGetModuleRepository
    {
        Task<ModuleDto> GetModule(string moduleId, string organizationId, CancellationToken token);
    }
}