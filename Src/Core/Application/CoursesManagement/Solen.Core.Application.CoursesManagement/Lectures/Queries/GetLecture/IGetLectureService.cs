using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Lectures.Queries
{
    public interface IGetLectureService
    {
        Task<LectureDto> GetLecture(string lectureId, CancellationToken token);
    }
}