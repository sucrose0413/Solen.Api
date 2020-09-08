using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public interface INoModuleErrorRepository
    {
        Task<bool> DoesCourseHaveModules(string courseId, CancellationToken token); 
    }
}