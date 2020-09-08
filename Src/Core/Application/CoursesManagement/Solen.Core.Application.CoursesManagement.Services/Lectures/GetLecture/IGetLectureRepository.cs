using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Lectures
{
    public interface IGetLectureRepository
    {
        Task<LectureDto> GetLecture(string lectureId, string organizationId, CancellationToken token);
    }
}