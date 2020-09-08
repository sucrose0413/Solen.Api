using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetLearningPathLearnersService : IGetLearningPathLearnersService
    {
        private readonly IGetLearningPathLearnersRepository _repo;

        public GetLearningPathLearnersService(IGetLearningPathLearnersRepository repo)
        {
            _repo = repo;
        }

        public async Task<IList<LearnerForLearningPathDto>> GetLearningPathLearners(string learningPathId,
            CancellationToken token)
        {
            return await _repo.GetLearningPathLearners(learningPathId, token);
        }
    }
}