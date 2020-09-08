using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public interface IGetLearningPathLearnersRepository
    {
        Task<IList<LearnerForLearningPathDto>> GetLearningPathLearners(string learningPathId, CancellationToken token);
    }
}