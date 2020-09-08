using MediatR;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Commands
{
    public class PasswordTokenSetEvent : INotification
    {
        public PasswordTokenSetEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}