using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public interface IGetLearningPathsRepository
    {
        Task<IList<LearningPathDto>> GetLearningPaths(string organizationId, CancellationToken token);
    }
}