using MediatR;

namespace Solen.Core.Application.CoursesManagement.LearningPaths.Queries
{
    public class GetCourseLearningPathsListQuery : IRequest<GetCourseLearningPathsListVm>
    {
        public string CourseId { get; set; }
    }
}