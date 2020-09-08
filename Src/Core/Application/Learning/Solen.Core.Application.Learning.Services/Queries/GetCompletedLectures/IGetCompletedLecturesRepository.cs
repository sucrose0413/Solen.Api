using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public interface IGetCompletedLecturesRepository
    {
        Task<IList<string>> GetLearnerCompletedLectures(string courseId, string learnerId, CancellationToken token);
    }
}