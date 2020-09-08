using MediatR;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserPasswordCommand : IRequest
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}