using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class
        GetLearningPathLearnersQueryHandler : IRequestHandler<GetLearningPathLearnersQuery,
            LearningPathLearnersViewModel>
    {
        private readonly IGetLearningPathLearnersService _service;

        public GetLearningPathLearnersQueryHandler(IGetLearningPathLearnersService service)
        {
            _service = service;
        }

        public async Task<LearningPathLearnersViewModel> Handle(GetLearningPathLearnersQuery query,
            CancellationToken token)
        {
            return new LearningPathLearnersViewModel
            {
                Learners = await _service.GetLearningPathLearners(query.LearningPathId, token)
            };
        }
    }
}