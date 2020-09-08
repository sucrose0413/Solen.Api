using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Queries
{
    public interface IGetCoursesListService
    {
        Task<LearnerCoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery, CancellationToken token);
    }
}