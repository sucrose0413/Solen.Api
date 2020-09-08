using MediatR;


namespace Solen.Core.Application.Learning.Queries
{
    public class GetCoursesListQuery : QueryObject, IRequest<LearnerCoursesListViewModel>
    {
        public string AuthorId { get; set; }
    }
}