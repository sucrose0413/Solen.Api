using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public interface IGetCoursesFiltersRepository
    {
        Task<IList<CoursesManagementAuthorFilterDto>> GetCoursesAuthors(string organizationId, CancellationToken token);
        Task<IList<LearningPathFilterDto>> GetLearningPaths(string organizationId, CancellationToken token);
    }
}