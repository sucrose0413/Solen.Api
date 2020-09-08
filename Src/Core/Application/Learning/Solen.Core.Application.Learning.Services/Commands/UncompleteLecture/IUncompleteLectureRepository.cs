using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public interface IUncompleteLectureRepository
    {
        Task<LearnerCompletedLecture> GetCompletedLecture(string learnerId, string lectureId, CancellationToken token);
        void RemoveLearnerCompletedLecture(LearnerCompletedLecture lecture);
    }
}