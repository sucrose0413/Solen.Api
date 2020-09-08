using MediatR;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.SigningUp.Users.Commands
{
    public class UserSigningUpCompletedEventNotification : INotification
    {
        public UserSigningUpCompletedEventNotification(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}