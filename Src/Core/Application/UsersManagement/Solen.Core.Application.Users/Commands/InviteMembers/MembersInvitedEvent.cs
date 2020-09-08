using System.Collections.Generic;
using MediatR;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public class MembersInvitedEvent : INotification
    {
        public MembersInvitedEvent(IEnumerable<User> users)
        {
            Users = users;
        }

        public IEnumerable<User> Users { get; }
    }
}
