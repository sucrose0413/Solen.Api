using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface IUpdateCoursesOrdersService
    {
        Task<List<LearningPathCourse>> GetLearningPathCourses(string learningPathId, CancellationToken token);
        void UpdateCoursesOrders(List<LearningPathCourse> courses, IEnumerable<CourseOrderDto> coursesNewOrders);
        void UpdateLearningPathCoursesRepo(IEnumerable<LearningPathCourse> courses);
    }
}