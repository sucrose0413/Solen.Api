using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetLearningPathCoursesService : IGetLearningPathCoursesService
    {
        private readonly IGetLearningPathCoursesRepository _repo;

        public GetLearningPathCoursesService(IGetLearningPathCoursesRepository repo)
        {
            _repo = repo;
        }  
        
        public async Task<IList<CourseForLearningPathDto>> GetLearningPathCourses(string learningPathId,
            CancellationToken token)
        {
            return await _repo.GetLearningPathCourses(learningPathId, token);
        }
    }
}