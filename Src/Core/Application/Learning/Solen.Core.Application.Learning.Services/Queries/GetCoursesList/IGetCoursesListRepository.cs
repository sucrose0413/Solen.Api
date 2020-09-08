using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Learning.Queries;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public interface IGetCoursesListRepository
    {
        Task<LearnerCoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery, string learnerId,
            string learningPathId, string publishedStatus, CancellationToken token);
    }
}