using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Learning.Queries;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public interface IGetCourseContentRepository
    {
        Task<LearnerCourseContentDto> GetCourseContentFromRepo(string courseId, string learningPathId,
            string publishedStatus, CancellationToken token);

        Task<string> GetLastLectureId(string courseId, string learnerId, CancellationToken token);
    }
}