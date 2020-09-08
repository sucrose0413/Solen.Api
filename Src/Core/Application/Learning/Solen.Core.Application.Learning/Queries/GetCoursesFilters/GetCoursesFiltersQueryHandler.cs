using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCoursesFiltersQueryHandler : IRequestHandler<GetCoursesFiltersQuery, LearnerCoursesFiltersViewModel>
    {
        private readonly IGetCoursesFiltersService _service;

        public GetCoursesFiltersQueryHandler(IGetCoursesFiltersService service)
        {
            _service = service;
        }

        public async Task<LearnerCoursesFiltersViewModel> Handle(GetCoursesFiltersQuery query, CancellationToken token)
        {
            return new LearnerCoursesFiltersViewModel
            {
                AuthorsFiltersList = await _service.GetCoursesAuthors(token),
                OrderByFiltersList = _service.GetOrderByValues(),
                OrderByDefaultValue = _service.GetOrderByDefaultValue(),
            };
        }
    }
}