using MediatR;

namespace Solen.Core.Application.Auth.Commands
{
    public class ResetPasswordCommand : IRequest
    {
        public string PasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}