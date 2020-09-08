using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public interface INoLectureErrorsRepository
    {
        Task<IEnumerable<ModuleInErrorDto>> GetModulesWithoutLectures(string courseId, CancellationToken token);
    }
}