using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.LearningPaths.Queries
{
    public class GetCourseLearningPathsListQueryHandler : IRequestHandler<GetCourseLearningPathsListQuery,
        GetCourseLearningPathsListVm>

    {
        private readonly IGetCourseLearningPathsService _service;

        public GetCourseLearningPathsListQueryHandler(IGetCourseLearningPathsService service)
        {
            _service = service;
        }


        public async Task<GetCourseLearningPathsListVm> Handle(GetCourseLearningPathsListQuery query,
            CancellationToken token)
        {
            return new GetCourseLearningPathsListVm
            {
                CourseLearningPathsIds = await _service.GetCourseLearningPathsIds(query.CourseId, token),
                LearningPaths = await _service.GetOrganizationLearningPaths(token)
            };
        }
    }
}