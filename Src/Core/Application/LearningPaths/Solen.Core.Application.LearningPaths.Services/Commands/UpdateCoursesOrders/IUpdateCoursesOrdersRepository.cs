using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface IUpdateCoursesOrdersRepository
    {
        Task<List<LearningPathCourse>> GetLearningPathCourses(string learningPathId, string organizationId,
            CancellationToken token);

        void UpdateLearningPathCourses(IEnumerable<LearningPathCourse> courses);
    }
}