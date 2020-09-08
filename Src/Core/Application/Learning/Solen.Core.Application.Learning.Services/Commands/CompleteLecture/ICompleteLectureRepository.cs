using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public interface ICompleteLectureRepository
    {
        Task<bool> DoesLectureExist(string lectureId, string learningPathId, CancellationToken token);
        Task<bool> IsTheLectureAlreadyCompleted(string lectureId, string learnerId, CancellationToken token);
        void AddLearnerCompletedLecture(LearnerCompletedLecture lecture);
    }
}