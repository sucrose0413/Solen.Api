using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Lectures.Queries
{
    public class GetLectureQueryHandler : IRequestHandler<GetLectureQuery, LectureViewModel>
    {
        private readonly IGetLectureService _service;

        public GetLectureQueryHandler(IGetLectureService service)
        {
            _service = service;
        }

        public async Task<LectureViewModel> Handle(GetLectureQuery query, CancellationToken token)
        {
            var lecture = await _service.GetLecture(query.LectureId, token);

            return new LectureViewModel(lecture);
        }
    }
}