using MediatR;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class SigningUpCompletedEvent : INotification
    {
        public SigningUpCompletedEvent(Organization organization, User user)
        {
            Organization = organization;
            User = user;
        }

        public Organization Organization { get; }
        public User User { get; }
    }
}