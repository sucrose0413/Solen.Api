using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Commands
{
    public interface IUncompleteLectureService
    {
        Task<LearnerCompletedLecture> GetCompletedLecture(string lectureId, CancellationToken token);
        void RemoveLearnerCompletedLectureFromRepo(LearnerCompletedLecture lecture);
    }
}