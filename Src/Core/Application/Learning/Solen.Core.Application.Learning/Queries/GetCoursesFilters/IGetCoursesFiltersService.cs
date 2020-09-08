using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Queries
{
    public interface IGetCoursesFiltersService
    {
        Task<IList<LearnerCourseAuthorFilterDto>> GetCoursesAuthors(CancellationToken token);
        IList<LearnerCourseOrderByDto> GetOrderByValues();
        int GetOrderByDefaultValue();
    }
}