using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public class
        GetNotificationTemplateQueryHandler : IRequestHandler<GetNotificationTemplateQuery,
            NotificationTemplateViewModel>
    {
        private readonly IGetNotificationTemplateService _service;

        public GetNotificationTemplateQueryHandler(IGetNotificationTemplateService service)
        {
            _service = service;
        }

        public async Task<NotificationTemplateViewModel> Handle(GetNotificationTemplateQuery query,
            CancellationToken token)
        {
            return new NotificationTemplateViewModel
            {
                NotificationTemplate = await _service.GetNotificationTemplate(query.TemplateId, token)
            };
        }
    }
}