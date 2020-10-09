using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoModuleErrorHandler : ICourseErrors
    {
        private readonly INoModuleErrorRepository _repo;

        public NoModuleErrorHandler(INoModuleErrorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token)
        {
            var courseErrors = new List<CourseErrorDto>();

            if (!await _repo.DoesCourseHaveModules(courseId, token))
                courseErrors.Add(new CourseErrorDto {Error = NoModuleError.Instance.Name});

            return courseErrors;
        }
    }
}