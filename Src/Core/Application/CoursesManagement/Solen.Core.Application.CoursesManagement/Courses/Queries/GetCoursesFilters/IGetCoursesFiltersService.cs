using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public interface IGetCoursesFiltersService
    {
        Task<IList<CoursesManagementAuthorFilterDto>> GetCoursesAuthors(CancellationToken token);
        Task<IList<LearningPathFilterDto>> GetLearningPaths(CancellationToken token);
        IList<CoursesManagementOrderByDto> GetOrderByValues();
        int GetOrderByDefaultValue();
        IList<StatusFilterDto> GetStatus();
    }
}