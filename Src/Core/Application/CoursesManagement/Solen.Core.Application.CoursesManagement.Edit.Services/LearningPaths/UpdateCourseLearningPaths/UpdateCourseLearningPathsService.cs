using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.LearningPaths
{
    public class UpdateCourseLearningPathsService : IUpdateCourseLearningPathsService
    {
        private readonly IUpdateCourseLearningPathsRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateCourseLearningPathsService(IUpdateCourseLearningPathsRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Course> GetCourseFromRepo(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseWithLearningPaths(courseId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The course ({courseId}) does not exist");
        }

        public void CheckCourseStatusForModification(Course course)
        {
            if (!course.IsEditable)
                throw new UnalterableCourseException();
        }

        public void RemoveEventualLearningPaths(Course course, IList<string> newLearningPathsIds)
        {
            var learningPathsToRemove =
                course.CourseLearningPaths.Where(l => !newLearningPathsIds.Contains(l.LearningPathId))
                    .ToList();

            foreach (var learningPathCourse in learningPathsToRemove)
                course.RemoveLearningPath(learningPathCourse);
        }

        public IEnumerable<string> GetLearningPathsIdsToAdd(Course course, IEnumerable<string> newLearningPathsIds)
        {
            return newLearningPathsIds.Where(id =>
                course.CourseLearningPaths.All(c => c.LearningPathId != id)).ToList();
        }

        public async Task AddLearningPathToCourse(Course course, string newLearningPathId,
            CancellationToken token)
        {
            await CheckLearningPathExistence(newLearningPathId, token);
            var courseOrder = await _repo.GetLearningPathLastOrder(newLearningPathId, token);
            if (courseOrder != null)
            {
                var courseLearningPath = new LearningPathCourse(newLearningPathId, course.Id, courseOrder.Value + 1);
                course.AddLearningPath(courseLearningPath);
            }
        }

        public void UpdateCourseRepo(Course course)
        {
            _repo.UpdateCourseLearningPaths(course);
        }

        #region Private Methods

        private async Task CheckLearningPathExistence(string learningPathId, CancellationToken token)
        {
            if (!await _repo.DoesLearningPathExist(learningPathId, _currentUserAccessor.OrganizationId, token))
                throw new NotFoundException(nameof(LearningPath), learningPathId);
        }

        #endregion
    }
}