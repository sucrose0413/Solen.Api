using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCoursesFiltersQueryHandler : IRequestHandler<GetCoursesFiltersQuery, CoursesFiltersViewModel>
    {
        private readonly IGetCoursesFiltersService _service;

        public GetCoursesFiltersQueryHandler(IGetCoursesFiltersService service)
        {
            _service = service;
        }

        public async Task<CoursesFiltersViewModel> Handle(GetCoursesFiltersQuery query, CancellationToken token)
        {
            return new CoursesFiltersViewModel
            {
                AuthorsFiltersList = await _service.GetCoursesAuthors(token),
                LearningPathsFiltersList = await _service.GetLearningPaths(token),
                OrderByFiltersList = _service.GetOrderByValues(),
                OrderByDefaultValue = _service.GetOrderByDefaultValue(),
                StatusFiltersList = _service.GetStatus()
            };
        }
    }
}