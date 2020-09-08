using MediatR;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class CompleteOrganizationSigningUpCommand : IRequest
    {
        public string SigningUpToken { get; set; }
        public string OrganizationName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}