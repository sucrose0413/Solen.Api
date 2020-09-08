using MediatR;

namespace Solen.Core.Application.Auth.Queries
{
    public class GetCurrentLoggedUserQuery : IRequest<LoggedUserDto>
    {
    }
}