using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Common.Notifications.Impl
{
    public class NotificationMessageHandler : INotificationMessageHandler
    {
        private readonly INotificationsRepo _repo;
        private readonly INotificationMessageGenerator _messageGenerator;
        private readonly INotificationMessageDispatcher _messageDispatcher;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public NotificationMessageHandler(INotificationsRepo repo, INotificationMessageGenerator messageGenerator,
            INotificationMessageDispatcher messageDispatcher, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _messageGenerator = messageGenerator;
            _messageDispatcher = messageDispatcher;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task Handle(RecipientContactInfo recipient, NotificationEvent @event, NotificationData data = null)
        {
            var excludedTemplates = _repo.GetDisabledNotifications(_currentUserAccessor.OrganizationId);

            var templates = _repo.GetNotificationTemplatesByNotificationEvent(@event, excludedTemplates);

            foreach (var template in templates)
            {
                var message = _messageGenerator.Generate(template, data);
                await _messageDispatcher.Dispatch(template.Type, message, recipient);
            }
        }
    }
}