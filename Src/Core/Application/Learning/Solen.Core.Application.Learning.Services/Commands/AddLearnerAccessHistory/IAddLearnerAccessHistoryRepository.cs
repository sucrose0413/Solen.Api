using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public interface IAddLearnerAccessHistoryRepository
    {
        Task<string> GetLectureCourseId(string lectureId, string learningPathId, CancellationToken token);
        void AddLearnerLectureAccessHistory(LearnerLectureAccessHistory history);
        void AddLearnerCourseAccessTime(LearnerCourseAccessTime access);
        void UpdateLearnerCourseAccessTime(LearnerCourseAccessTime access);
        Task<LearnerCourseAccessTime> GetLearnerCourseAccessTime(string learnerId, string courseId, CancellationToken token);
    }
}