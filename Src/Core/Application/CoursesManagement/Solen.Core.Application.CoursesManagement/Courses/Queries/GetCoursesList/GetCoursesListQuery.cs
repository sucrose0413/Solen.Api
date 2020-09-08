using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCoursesListQuery : QueryObject, IRequest<CoursesListViewModel>
    {
        public string AuthorId { get; set; }
        public string LearningPathId { get; set; }
        public int StatusId { get; set; }
    }

    public class PatghRequest
    {
        public string path_UI { get; set; }
        public string begin_date { get; set; }
        public string end_date { get; set; }
    }
}