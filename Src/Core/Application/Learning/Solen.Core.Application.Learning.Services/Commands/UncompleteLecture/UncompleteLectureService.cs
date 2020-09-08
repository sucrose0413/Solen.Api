using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public class UncompleteLectureService : IUncompleteLectureService
    {
        private readonly IUncompleteLectureRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UncompleteLectureService(IUncompleteLectureRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearnerCompletedLecture> GetCompletedLecture(string lectureId, CancellationToken token)
        {
            return await _repo.GetCompletedLecture(_currentUserAccessor.UserId, lectureId, token);
        }

        public void RemoveLearnerCompletedLectureFromRepo(LearnerCompletedLecture lecture)
        {
            _repo.RemoveLearnerCompletedLecture(lecture);
        }
    }
}