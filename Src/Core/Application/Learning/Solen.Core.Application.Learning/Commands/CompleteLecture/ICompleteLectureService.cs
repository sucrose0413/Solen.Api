using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Commands
{
    public interface ICompleteLectureService
    {
        Task CheckLectureExistence(string lectureId, CancellationToken token);
        Task<bool> IsTheLectureAlreadyCompleted(string lectureId, CancellationToken token);
        LearnerCompletedLecture CreateCompletedLecture(string lectureId);
        void AddLearnerCompletedLectureToRepo(LearnerCompletedLecture lecture);
    }
}