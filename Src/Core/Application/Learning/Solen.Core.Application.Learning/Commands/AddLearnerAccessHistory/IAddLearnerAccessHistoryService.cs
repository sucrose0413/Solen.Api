using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Commands
{
    public interface IAddLearnerAccessHistoryService
    {
        Task<string> GetLectureCourseId(string lectureId, CancellationToken token);
        LearnerLectureAccessHistory CreateAccessHistory(string lectureId);
        void AddLearnerLectureAccessHistoryToRepo(LearnerLectureAccessHistory history);
        Task UpdateOrCreateLearnerCourseAccessTime(string courseId, CancellationToken token);
    }
}