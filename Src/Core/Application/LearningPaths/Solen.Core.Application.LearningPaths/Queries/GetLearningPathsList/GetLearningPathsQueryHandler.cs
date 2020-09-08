using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathsQueryHandler : IRequestHandler<GetLearningPathsQuery, LearningPathsViewModel>
    {
        private readonly IGetLearningPathsService _service;

        public GetLearningPathsQueryHandler(IGetLearningPathsService service)
        {
            _service = service;
        }

        public async Task<LearningPathsViewModel> Handle(GetLearningPathsQuery query, CancellationToken token)
        {
            return new LearningPathsViewModel
            {
                LearningPaths = await _service.GetLearningPaths(token)
            };
        }
    }
}