using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class
        GetLearningPathsInfoQueryHandler : IRequestHandler<GetLearningPathsInfoQuery, LearningPathsInfoViewModel>
    {
        private readonly IGetLearningPathsInfoService _service;

        public GetLearningPathsInfoQueryHandler(IGetLearningPathsInfoService service)
        {
            _service = service;
        }

        public async Task<LearningPathsInfoViewModel> Handle(GetLearningPathsInfoQuery query, CancellationToken token)
        {
            return new LearningPathsInfoViewModel
            {
                LearningPaths = await _service.GetLearningPaths(token)
            };
        }
    }
}