using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetLearnerInfoQueryHandler : IRequestHandler<GetLearnerInfoQuery, LearnerInfoViewModel>
    {
        private readonly IGetLearnerInfoService _service;

        public GetLearnerInfoQueryHandler(IGetLearnerInfoService service)
        {
            _service = service;
        }

        public async Task<LearnerInfoViewModel> Handle(GetLearnerInfoQuery query, CancellationToken token)
        {
            var lastCourse = await _service.GetLastCourse(token);
            return new LearnerInfoViewModel
            {
                LastCourseProgress = await _service.GetLastCourseProgress(lastCourse, token)
            };
        }
    }
}