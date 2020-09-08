using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetLearnerInfoQuery : IRequest<LearnerInfoViewModel>
    {
    }
}