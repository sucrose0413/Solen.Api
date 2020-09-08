using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseContentQuery : IRequest<LearnerCourseContentViewModel>
    {
        public string CourseId { get; set; }
    }
}
