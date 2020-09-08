using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Queries
{
    public interface IGetCompletedLecturesService
    {
        Task<IList<string>> GetLearnerCompletedLectures(string courseId, CancellationToken token);
    }
}