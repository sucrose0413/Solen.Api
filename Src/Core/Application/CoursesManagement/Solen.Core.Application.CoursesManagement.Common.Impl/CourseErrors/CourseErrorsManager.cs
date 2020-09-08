using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class CourseErrorsManager : ICourseErrorsManager
    {
        private readonly List<ICourseErrors> _courseErrorsHandlers;

        public CourseErrorsManager(List<ICourseErrors> courseErrorsHandlers)
        {
            _courseErrorsHandlers = courseErrorsHandlers ?? new List<ICourseErrors>();
        }

        public async Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token)
        {
            var courseErrors = new List<CourseErrorDto>();

            foreach (var courseErrorsHandler in _courseErrorsHandlers)
                courseErrors.AddRange(await courseErrorsHandler.GetCourseErrors(courseId, token));
            
            return courseErrors;
        }
    }
}