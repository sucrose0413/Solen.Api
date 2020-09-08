using MediatR;

namespace Solen.Core.Application.Users.Commands
{
    public class UnblockUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}