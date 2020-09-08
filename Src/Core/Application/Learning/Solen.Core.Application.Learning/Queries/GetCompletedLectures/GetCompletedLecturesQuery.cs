using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCompletedLecturesQuery : IRequest<CompletedLecturesViewModel>
    {
        public GetCompletedLecturesQuery(string courseId)
        {
            CourseId = courseId;
        }

        public string CourseId { get; }
    }
}