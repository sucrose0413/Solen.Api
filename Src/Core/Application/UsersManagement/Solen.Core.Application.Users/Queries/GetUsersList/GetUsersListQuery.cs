using MediatR;

namespace Solen.Core.Application.Users.Queries
{
    public class GetUsersListQuery : IRequest<UsersListViewModel>
    {
    }
}