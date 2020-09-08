using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public class CompleteLectureService : ICompleteLectureService
    {
        private readonly ICompleteLectureRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CompleteLectureService(ICompleteLectureRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task CheckLectureExistence(string lectureId, CancellationToken token)
        {
            if (!await _repo.DoesLectureExist(lectureId, _currentUserAccessor.LearningPathId, token))
                throw new NotFoundException($"The Lecture ({lectureId}) does not exist");
        }

        public async Task<bool> IsTheLectureAlreadyCompleted(string lectureId, CancellationToken token)
        {
            return await _repo.IsTheLectureAlreadyCompleted(lectureId, _currentUserAccessor.UserId, token);
        }

        public LearnerCompletedLecture CreateCompletedLecture(string lectureId)
        {
            return new LearnerCompletedLecture(_currentUserAccessor.UserId, lectureId);
        }

        public void AddLearnerCompletedLectureToRepo(LearnerCompletedLecture lecture)
        {
            _repo.AddLearnerCompletedLecture(lecture);
        }
    }
}