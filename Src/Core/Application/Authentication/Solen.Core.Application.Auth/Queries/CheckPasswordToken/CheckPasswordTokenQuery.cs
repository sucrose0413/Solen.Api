using MediatR;

namespace Solen.Core.Application.Auth.Queries
{
    public class CheckPasswordTokenQuery : IRequest
    {
        public string PasswordToken { get; set; }
    }
}