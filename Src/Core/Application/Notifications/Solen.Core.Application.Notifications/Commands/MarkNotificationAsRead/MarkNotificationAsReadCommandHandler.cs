using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Notifications.Commands
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand>
    {
        private readonly IMarkNotificationAsReadService _service;
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadCommandHandler(IMarkNotificationAsReadService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(MarkNotificationAsReadCommand command, CancellationToken token)
        {
            var notification = await _service.GetNotification(command.NotificationId, token);

            if (notification == null)
                return Unit.Value;

            _service.MarkNotificationAsRead(notification);

            _service.UpdateNotificationRepo(notification);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}