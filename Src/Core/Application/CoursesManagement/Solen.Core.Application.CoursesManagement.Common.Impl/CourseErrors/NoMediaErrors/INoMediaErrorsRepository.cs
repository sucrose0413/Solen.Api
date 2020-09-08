using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public interface INoMediaErrorsRepository
    {
        Task<IEnumerable<LectureInErrorDto>> GetMediaLecturesWithoutUrl(string courseId, CancellationToken token);
    }
}