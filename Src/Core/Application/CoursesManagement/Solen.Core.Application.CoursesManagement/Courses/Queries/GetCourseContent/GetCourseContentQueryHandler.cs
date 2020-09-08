using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseContentQueryHandler : IRequestHandler<GetCourseContentQuery, CourseContentViewModel>
    {
        private readonly IGetCourseContentService _service;

        public GetCourseContentQueryHandler(IGetCourseContentService service)
        {
            _service = service;
        }

        public async Task<CourseContentViewModel> Handle(GetCourseContentQuery query, CancellationToken token)
        {
            var courseContent = await _service.GetCourseContent(query.CourseId, token);

            return new CourseContentViewModel {CourseContent = courseContent};
        }
    }
}