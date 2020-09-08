using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Notifications.Queries
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, NotificationsListViewModel>
    {
        private readonly IGetNotificationsService _service;

        public GetNotificationsQueryHandler(IGetNotificationsService service)
        {
            _service = service;
        }

        public async Task<NotificationsListViewModel> Handle(GetNotificationsQuery query, CancellationToken token)
        {
            return new NotificationsListViewModel
            {
                Notifications = await _service.GetNotifications(token)
            };
        }
    }
}