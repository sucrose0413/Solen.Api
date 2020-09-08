using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearnerProgressQueryHandler : IRequestHandler<GetLearnerProgressQuery, LearnerProgressViewModel>
    {
        private readonly IGetLearnerProgressService _service;

        public GetLearnerProgressQueryHandler(IGetLearnerProgressService service)
        {
            _service = service;
        }
        
        public async Task<LearnerProgressViewModel> Handle(GetLearnerProgressQuery query, CancellationToken token)
        {
            var learner = await _service.GetLearner(query.LearnerId, token);
            
            return new LearnerProgressViewModel
            {
                LearnerCompletedCourses = await _service.GetLearnerCompletedCourses(learner, token)
            };
        }
    }
}