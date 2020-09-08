using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common
{
    public interface ICourseErrorsManager
    {
        Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token);
    }
}