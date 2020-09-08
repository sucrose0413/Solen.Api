using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public interface IGetCourseService
    {
        Task<CourseDto> GetCourse(string courseId, CancellationToken token);
        Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token);
    }
}
