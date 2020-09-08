using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetLearningPathLearnersService
    {
        Task<IList<LearnerForLearningPathDto>> GetLearningPathLearners(string learningPathId, CancellationToken token);
    }
}