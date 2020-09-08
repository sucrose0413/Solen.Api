using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCoursesListQueryHandler : IRequestHandler<GetCoursesListQuery, CoursesListViewModel>
    {
        private readonly IGetCoursesListService _service;
        
        public GetCoursesListQueryHandler(IGetCoursesListService service)
        {
            _service = service;
        }

        public async Task<CoursesListViewModel> Handle(GetCoursesListQuery query, CancellationToken token)
        {
            return new CoursesListViewModel
            {
                QueryResult = await _service.GetCoursesList(query, token)
            };
        }
    }
}