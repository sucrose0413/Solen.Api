using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetOtherCoursesToAddService
    {
        Task<IList<CourseForLearningPathDto>> GetOtherCoursesToAdd(string learningPathId, CancellationToken token);
    }
}