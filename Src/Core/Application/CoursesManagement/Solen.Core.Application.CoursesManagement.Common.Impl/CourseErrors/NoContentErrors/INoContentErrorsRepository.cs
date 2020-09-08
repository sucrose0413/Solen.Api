using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public interface INoContentErrorsRepository
    {
        Task<IEnumerable<LectureInErrorDto>> GetArticleLecturesWithoutContent(string courseId, CancellationToken token);
    }
}