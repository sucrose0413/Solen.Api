using MediatR;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCoursesFiltersQuery : IRequest<LearnerCoursesFiltersViewModel>
    {
    }
}