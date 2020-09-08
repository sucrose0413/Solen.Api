using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class
        GetLearningPathCoursesQueryHandler : IRequestHandler<GetLearningPathCoursesQuery, LearningPathCoursesViewModel>
    {
        private readonly IGetLearningPathCoursesService _service;

        public GetLearningPathCoursesQueryHandler(IGetLearningPathCoursesService service)
        {
            _service = service;
        }

        public async Task<LearningPathCoursesViewModel> Handle(GetLearningPathCoursesQuery query,
            CancellationToken token)
        {
            return new LearningPathCoursesViewModel
            {
                Courses = await _service.GetLearningPathCourses(query.LearningPathId, token)
            };
        }
    }
}