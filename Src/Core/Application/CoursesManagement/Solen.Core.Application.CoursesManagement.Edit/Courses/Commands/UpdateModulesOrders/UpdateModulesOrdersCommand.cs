using System.Collections.Generic;
using MediatR;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateModulesOrdersCommand : IRequest
    {
        public string CourseId { get; set; }
        public IEnumerable<ModuleOrderDto> ModulesOrders { get; set; } = new List<ModuleOrderDto>();
    }
}