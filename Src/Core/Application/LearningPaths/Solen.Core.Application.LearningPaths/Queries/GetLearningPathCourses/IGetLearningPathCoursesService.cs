using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetLearningPathCoursesService
    {
        Task<IList<CourseForLearningPathDto>> GetLearningPathCourses(string learningPathId, CancellationToken token);
    }
}