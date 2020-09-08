using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class
        GetOtherCoursesToAddQueryHandler : IRequestHandler<GetOtherCoursesToAddQuery, OtherCoursesToAddViewModel>
    {
        private readonly IGetOtherCoursesToAddService _service;

        public GetOtherCoursesToAddQueryHandler(IGetOtherCoursesToAddService service)
        {
            _service = service;
        }

        public async Task<OtherCoursesToAddViewModel> Handle(GetOtherCoursesToAddQuery query, CancellationToken token)
        {
            return new OtherCoursesToAddViewModel
            {
                Courses = await _service.GetOtherCoursesToAdd(query.LearningPathId, token)
            };
        }
    }
}