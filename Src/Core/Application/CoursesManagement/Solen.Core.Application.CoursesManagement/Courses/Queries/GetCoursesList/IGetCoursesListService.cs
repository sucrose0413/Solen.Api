using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public interface IGetCoursesListService
    {
        Task<CoursesListResult> GetCoursesList(GetCoursesListQuery query, CancellationToken token);
    }
}