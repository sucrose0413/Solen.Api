using MediatR;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserInfoCommand : IRequest
    {
        public string UserName { get; set; }
    }
}