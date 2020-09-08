using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface IUpdateModulesOrdersService
    {
        Task<List<Module>> GetCourseModulesFromRepo(string courseId, CancellationToken token);
        void UpdateModulesOrders(List<Module> modules, IEnumerable<ModuleOrderDto> modulesNewOrders);
        void UpdateModulesRepo(IEnumerable<Module> modules);
    }
}