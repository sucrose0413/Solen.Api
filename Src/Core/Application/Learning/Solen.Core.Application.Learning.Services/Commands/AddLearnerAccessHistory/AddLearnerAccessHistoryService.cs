using System;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Services.Commands
{
    public class AddLearnerAccessHistoryService : IAddLearnerAccessHistoryService
    {
        private readonly IAddLearnerAccessHistoryRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public AddLearnerAccessHistoryService(IAddLearnerAccessHistoryRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<string> GetLectureCourseId(string lectureId, CancellationToken token)
        {
            return await _repo.GetLectureCourseId(lectureId, _currentUserAccessor.LearningPathId, token) ??
                   throw new NotFoundException($"The Lecture ({lectureId}) does not exist");
        }

        public LearnerLectureAccessHistory CreateAccessHistory(string lectureId)
        {
            return new LearnerLectureAccessHistory(_currentUserAccessor.UserId, lectureId);
        }

        public void AddLearnerLectureAccessHistoryToRepo(LearnerLectureAccessHistory history)
        {
            _repo.AddLearnerLectureAccessHistory(history);
        }

        public async Task UpdateOrCreateLearnerCourseAccessTime(string courseId, CancellationToken token)
        {
            var accessTime = await _repo.GetLearnerCourseAccessTime(_currentUserAccessor.UserId, courseId, token);

            if (accessTime == null)
            {
                accessTime = new LearnerCourseAccessTime(_currentUserAccessor.UserId, courseId);
                _repo.AddLearnerCourseAccessTime(accessTime);
            }
            else
            {
                accessTime.UpdateAccessTime(DateTime.Now);
                _repo.UpdateLearnerCourseAccessTime(accessTime);
            }
        }
    }
}