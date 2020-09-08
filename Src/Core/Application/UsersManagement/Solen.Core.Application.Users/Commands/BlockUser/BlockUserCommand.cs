using MediatR;

namespace Solen.Core.Application.Users.Commands
{
    public class BlockUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}