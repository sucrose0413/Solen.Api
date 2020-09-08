using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public class
        GetNotificationTemplatesQueryHandler : IRequestHandler<GetNotificationTemplatesQuery,
            NotificationTemplatesViewModel>
    {
        private readonly IGetNotificationTemplatesService _service;

        public GetNotificationTemplatesQueryHandler(IGetNotificationTemplatesService service)
        {
            _service = service;
        }

        public async Task<NotificationTemplatesViewModel> Handle(GetNotificationTemplatesQuery query,
            CancellationToken token)
        {
            return new NotificationTemplatesViewModel
            {
                NotificationTemplates = await _service.GetNotificationTemplates(token)
            };
        }
    }
}