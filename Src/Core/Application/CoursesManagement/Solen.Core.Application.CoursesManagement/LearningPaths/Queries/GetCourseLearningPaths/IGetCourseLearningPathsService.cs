using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.LearningPaths.Queries
{
    public interface IGetCourseLearningPathsService
    {
        Task<IList<string>> GetCourseLearningPathsIds(string courseId, CancellationToken token);

        Task<IList<CourseLearningPathDto>> GetOrganizationLearningPaths(CancellationToken token);
    }
}