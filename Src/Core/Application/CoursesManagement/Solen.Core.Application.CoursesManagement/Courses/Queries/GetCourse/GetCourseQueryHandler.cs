using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseViewModel>
    {
        private readonly IGetCourseService _service;

        public GetCourseQueryHandler(IGetCourseService service)
        {
            _service = service;
        }

        public async Task<CourseViewModel> Handle(GetCourseQuery query, CancellationToken token)
        {
            var course = await _service.GetCourse(query.CourseId, token);

            var courseErrors = await _service.GetCourseErrors(query.CourseId, token);

            return new CourseViewModel {Course = course, CourseErrors = courseErrors};
        }
    }
}