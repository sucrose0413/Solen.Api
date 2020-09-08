using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.Users.Commands
{
    public class InviteMembersCommand : IRequest
    {
        public IEnumerable<string> Emails { get; set; }
        public string LearningPathId { get; set; }
    }
}
