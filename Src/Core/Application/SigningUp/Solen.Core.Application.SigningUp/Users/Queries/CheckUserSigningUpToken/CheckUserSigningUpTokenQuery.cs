using MediatR;

namespace Solen.Core.Application.SigningUp.Users.Queries
{
    public class CheckUserSigningUpTokenQuery : IRequest
    {
        public string SigningUpToken { get; set; }
    }
}