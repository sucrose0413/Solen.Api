using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public interface IGetCoursesListRepository
    {
        Task<CoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery, string organizationId,
            CancellationToken token);
    }
}