using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class
        GetCompletedLecturesQueryHandler : IRequestHandler<GetCompletedLecturesQuery, CompletedLecturesViewModel>
    {
        private readonly IGetCompletedLecturesService _service;

        public GetCompletedLecturesQueryHandler(IGetCompletedLecturesService service)
        {
            _service = service;
        }

        public async Task<CompletedLecturesViewModel> Handle(GetCompletedLecturesQuery query, CancellationToken token)
        {
            return new CompletedLecturesViewModel
            {
                CompletedLectures = await _service.GetLearnerCompletedLectures(query.CourseId, token)
            };
        }
    }
}