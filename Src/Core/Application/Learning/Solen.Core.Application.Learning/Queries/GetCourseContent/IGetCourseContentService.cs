using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Queries
{
    public interface IGetCourseContentService
    {
        Task<LearnerCourseContentDto> GetCourseContentFromRepo(string courseId, CancellationToken token);
        Task<string> GetLastLectureId(string courseId, CancellationToken token);
    }
}