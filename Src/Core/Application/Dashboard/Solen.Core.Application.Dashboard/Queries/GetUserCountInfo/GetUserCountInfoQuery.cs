using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetUserCountInfoQuery : IRequest<UserCountInfoViewModel>
    {
    }
}