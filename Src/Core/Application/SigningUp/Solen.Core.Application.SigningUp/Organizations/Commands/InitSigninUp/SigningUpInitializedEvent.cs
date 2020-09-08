using MediatR;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class SigningUpInitializedEvent : INotification
    {
        public SigningUpInitializedEvent(OrganizationSigningUp signingUp)
        {
            SigningUp = signingUp;
        }

        public OrganizationSigningUp SigningUp { get; }
    }
}