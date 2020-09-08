using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateLecturesOrdersCommand : IRequest
    {
        public string ModuleId { get; set; }
        public IEnumerable<LectureOrderDto> LecturesOrders { get; set; } = new List<LectureOrderDto>();
    }
}
