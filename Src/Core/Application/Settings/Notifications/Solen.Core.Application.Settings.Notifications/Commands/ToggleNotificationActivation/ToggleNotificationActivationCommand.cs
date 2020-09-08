using MediatR;

namespace Solen.Core.Application.Settings.Notifications.Commands
{
    public class ToggleNotificationActivationCommand : IRequest
    {
        public string TemplateId { get; set; }
        public bool IsActivated { get; set; }
    }
}