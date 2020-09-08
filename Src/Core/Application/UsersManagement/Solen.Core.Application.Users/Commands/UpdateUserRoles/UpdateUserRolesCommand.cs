using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserRolesCommand : IRequest
    {
        public string UserId { get; set; }
        public List<string> RolesIds { get; set; }
    }
}