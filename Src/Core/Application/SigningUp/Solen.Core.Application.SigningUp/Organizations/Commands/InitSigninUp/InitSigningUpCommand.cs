using MediatR;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class InitSigningUpCommand : IRequest
    {
        public string Email { get; set; }
    }
}