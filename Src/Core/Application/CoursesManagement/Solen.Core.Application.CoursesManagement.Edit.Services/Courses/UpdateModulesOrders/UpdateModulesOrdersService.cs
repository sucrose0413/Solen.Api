using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class UpdateModulesOrdersService : IUpdateModulesOrdersService
    {
        private readonly IUpdateModulesOrdersRepository _repo;

        public UpdateModulesOrdersService(IUpdateModulesOrdersRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Module>> GetCourseModulesFromRepo(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseModules(courseId, token);
        }

        public void UpdateModulesOrders(List<Module> modules, IEnumerable<ModuleOrderDto> modulesNewOrders)
        {
            var modulesOrdersArray = modulesNewOrders as ModuleOrderDto[] ?? modulesNewOrders.ToArray();

            modules.ForEach(x =>
            {
                var order = modulesOrdersArray.Any(m => m.ModuleId == x.Id)
                    ? modulesOrdersArray.First(m => m.ModuleId == x.Id).Order
                    : x.Order;

                x.UpdateOrder(order);
            });
        }

        public void UpdateModulesRepo(IEnumerable<Module> modules)
        {
            _repo.UpdateModules(modules);
        }
    }
}