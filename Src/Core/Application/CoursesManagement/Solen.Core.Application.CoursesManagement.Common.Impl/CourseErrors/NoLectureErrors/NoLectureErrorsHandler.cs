using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoLectureErrorsHandler : ICourseErrors
    {
        private readonly INoLectureErrorsRepository _repo;

        public NoLectureErrorsHandler(INoLectureErrorsRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token)
        {
            var modules = await _repo.GetModulesWithoutLectures(courseId, token);
            var errors = modules.Select(x => new CourseErrorDto
                    {ModuleId = x.ModuleId, Error = $"Module {x.Order}: {NoLectureError.Instance.Name}"})
                .ToList();

            return errors;
        }
    }
}