using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.LearningPaths
{
    public interface IGetCourseLearningPathsRepository
    {
        Task<IList<string>> GetCourseLearningPathsIds(string courseId, string organizationId, CancellationToken token);

        Task<IList<CourseLearningPathDto>> GetOrganizationLearningPaths(string organizationId, CancellationToken token);
    }
}