using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetLearnerProgressService : IGetLearnerProgressService
    {
        private readonly IGetLearnerProgressRepository _repo;
        private readonly IUserManager _userManager;

        public GetLearnerProgressService(IGetLearnerProgressRepository repo, IUserManager userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<User> GetLearner(string learnerId, CancellationToken token)
        {
            return await _userManager.FindByIdAsync(learnerId) ??
                   throw new NotFoundException($" The Learner ({learnerId}) does not exist");
        }

        public async Task<LearnerCompletedCoursesDto> GetLearnerCompletedCourses(User learner, CancellationToken token)
        {
            if (learner.LearningPath == null)
                return new LearnerCompletedCoursesDto();

            var completedCourses = new List<string>();
            var publishedStatus = new PublishedStatus().Name;
            var courses = await _repo.GetLearningPathPublishedCourses(learner.LearningPathId, publishedStatus, token);
            foreach (var course in courses)
            {
                var completedLectures =
                    await _repo.GetLearnerCompletedLectures(learner.Id, course.CourseId, publishedStatus, token);

                if (completedLectures == course.LectureCount)
                    completedCourses.Add(course.CourseTitle);
            }

            return new LearnerCompletedCoursesDto
            {
                CompletedCourses = completedCourses,
                LearningPathCourseCount = courses.Count
            };
        }
    }
}