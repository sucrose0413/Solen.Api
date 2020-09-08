using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands
{
    public interface IUpdateCourseLearningPathsService
    {
        Task<Course> GetCourseFromRepo(string courseId, CancellationToken token);
        void CheckCourseStatusForModification(Course course);
        void RemoveEventualLearningPaths(Course course, IList<string> newLearningPathsIds);
        IEnumerable<string> GetLearningPathsIdsToAdd(Course course, IEnumerable<string> newLearningPathsIds);
        Task AddLearningPathToCourse(Course course, string newLearningPathId, CancellationToken token);
        void UpdateCourseRepo(Course course);
    }
}