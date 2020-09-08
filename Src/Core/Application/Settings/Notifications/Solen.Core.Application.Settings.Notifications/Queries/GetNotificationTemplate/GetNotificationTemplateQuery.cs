using MediatR;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public class GetNotificationTemplateQuery : IRequest<NotificationTemplateViewModel>
    {
        public GetNotificationTemplateQuery(string templateId)
        {
            TemplateId = templateId;
        }

        public string TemplateId { get; }
    }
}