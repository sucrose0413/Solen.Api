using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathQueryHandler : IRequestHandler<GetLearningPathQuery, LearningPathViewModel>
    {
        private readonly IGetLearningPathService _service;

        public GetLearningPathQueryHandler(IGetLearningPathService service)
        {
            _service = service;
        }

        public async Task<LearningPathViewModel> Handle(GetLearningPathQuery query, CancellationToken token)
        {
            return new LearningPathViewModel
            {
                LearningPath = await _service.GetLearningPath(query.LearningPathId, token),
            };
        }
    }
}