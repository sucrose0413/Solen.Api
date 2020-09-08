using MediatR;

namespace Solen.Core.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }
}