using MediatR;

namespace Solen.Core.Application.Notifications.Queries
{
    public class GetNotificationsQuery : IRequest<NotificationsListViewModel>, IRequest<Unit>
    {
    }
}
