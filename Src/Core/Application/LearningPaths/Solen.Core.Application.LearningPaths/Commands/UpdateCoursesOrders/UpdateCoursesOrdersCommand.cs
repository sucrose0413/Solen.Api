using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateCoursesOrdersCommand : IRequest
    {
        public string LearningPathId { get; set; }
        public IEnumerable<CourseOrderDto> CoursesOrders { get; set; } = new List<CourseOrderDto>();
    }
}