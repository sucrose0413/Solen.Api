using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetCoursesInfoQueryHandler : IRequestHandler<GetCoursesInfoQuery, CoursesInfoViewModel>
    {
        private readonly IGetCoursesInfoService _service;

        public GetCoursesInfoQueryHandler(IGetCoursesInfoService service)
        {
            _service = service;
        }

        public async Task<CoursesInfoViewModel> Handle(GetCoursesInfoQuery query, CancellationToken token)
        {
            return new CoursesInfoViewModel
            {
                CourseCount = await _service.GetCourseCount(token),
                LastCreatedCourse = await _service.GetLastCreatedCourse(token),
                LastPublishedCourse = await _service.GetLastPublishedCourse(token)
            };
        }
    }
}