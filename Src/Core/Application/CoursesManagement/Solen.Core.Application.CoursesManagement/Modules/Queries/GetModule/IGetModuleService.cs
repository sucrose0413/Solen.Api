using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Modules.Queries
{
    public interface IGetModuleService
    {
        Task<ModuleDto> GetModule(string moduleId, CancellationToken token);
    }
}