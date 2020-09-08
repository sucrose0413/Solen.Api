using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public interface IGetOtherCoursesToAddRepository
    {
        Task<IList<CourseForLearningPathDto>> GetCoursesToAdd(string learningPathId, string organizationId,
            CancellationToken token);
    }
}