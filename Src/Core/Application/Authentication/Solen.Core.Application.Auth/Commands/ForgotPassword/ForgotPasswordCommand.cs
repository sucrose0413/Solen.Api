using MediatR;

namespace Solen.Core.Application.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; }
    }
}