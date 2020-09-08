using MediatR;

namespace Solen.Core.Application.SigningUp.Users.Commands
{
    public class CompleteUserSigningUpCommand : IRequest
    {
        public string SigningUpToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}