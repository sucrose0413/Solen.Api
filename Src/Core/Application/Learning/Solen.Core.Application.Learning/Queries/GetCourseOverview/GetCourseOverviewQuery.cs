using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseOverviewQuery : IRequest<LearnerCourseOverviewViewModel>
    {
        public string CourseId { get; set; }
    }
}