using MediatR;


namespace Solen.Core.Application.Auth.Queries
{
    public class LoginUserQuery : IRequest<LoggedUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}