using MediatR;


namespace Solen.Core.Application.CoursesManagement.Lectures.Queries
{
    public class GetLectureQuery : IRequest<LectureViewModel>
    {
        public string LectureId { get; set; }
    }
}
