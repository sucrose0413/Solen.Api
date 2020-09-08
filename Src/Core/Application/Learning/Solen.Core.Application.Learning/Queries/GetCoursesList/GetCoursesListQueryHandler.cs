using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCoursesListQueryHandler : IRequestHandler<GetCoursesListQuery, LearnerCoursesListViewModel>
    {
        private readonly IGetCoursesListService _service;

        public GetCoursesListQueryHandler(IGetCoursesListService service)
        {
            _service = service;
        }

        public async Task<LearnerCoursesListViewModel> Handle(GetCoursesListQuery query, CancellationToken token)
        {
            return new LearnerCoursesListViewModel
            {
                QueryResult = await _service.GetCoursesList(query, token)
            };
        }
    }
}