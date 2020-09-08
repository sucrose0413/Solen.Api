using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoMediaErrorsHandler : ICourseErrors
    {
        private readonly INoMediaErrorsRepository _repo;

        public NoMediaErrorsHandler(INoMediaErrorsRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token)
        {
            var lectures = await _repo.GetMediaLecturesWithoutUrl(courseId, token);
            var errors = lectures.Select(x => new CourseErrorDto
            {
                ModuleId = x.ModuleId, LectureId = x.LectureId,
                Error = $"Lecture {x.ModuleOrder}.{x.Order}: {new NoMediaError().Name}"
            }).ToList();

            return errors;
        }
    }
}