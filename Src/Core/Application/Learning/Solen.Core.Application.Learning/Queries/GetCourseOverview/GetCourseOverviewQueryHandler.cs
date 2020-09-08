using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseOverviewQueryHandler : IRequestHandler<GetCourseOverviewQuery, LearnerCourseOverviewViewModel>
    {
        private readonly IGetCourseOverviewService _service;

        public GetCourseOverviewQueryHandler(IGetCourseOverviewService service)
        {
            _service = service;
        }

        public async Task<LearnerCourseOverviewViewModel> Handle(GetCourseOverviewQuery query, CancellationToken token)
        {
            return new LearnerCourseOverviewViewModel
            {
                CourseOverview = await _service.GetCourseOverview(query.CourseId, token)
            };
        }
    }
}