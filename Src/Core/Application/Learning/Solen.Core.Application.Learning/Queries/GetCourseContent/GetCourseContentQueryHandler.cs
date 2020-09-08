using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseContentQueryHandler : IRequestHandler<GetCourseContentQuery, LearnerCourseContentViewModel>
    {
        private readonly IGetCourseContentService _service;

        public GetCourseContentQueryHandler(IGetCourseContentService service)
        {
            _service = service;
        }

        public async Task<LearnerCourseContentViewModel> Handle(GetCourseContentQuery query, CancellationToken token)
        {
            return new LearnerCourseContentViewModel
            {
                CourseContent = await _service.GetCourseContentFromRepo(query.CourseId, token),
                LastLectureId = await _service.GetLastLectureId(query.CourseId, token)
            };
        }
    }
}