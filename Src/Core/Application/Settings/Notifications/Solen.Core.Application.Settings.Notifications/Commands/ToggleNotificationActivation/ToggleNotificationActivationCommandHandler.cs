using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Settings.Notifications.Commands
{
    public class ToggleNotificationActivationCommandHandler : IRequestHandler<ToggleNotificationActivationCommand, Unit>
    {
        private readonly IToggleNotificationActivationService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleNotificationActivationCommandHandler(IToggleNotificationActivationService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ToggleNotificationActivationCommand command, CancellationToken token)
        {
            if (command.IsActivated)
                await _service.ActivateNotificationTemplate(command.TemplateId, token);
            else
                await _service.DeactivateNotificationTemplate(command.TemplateId, token);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}