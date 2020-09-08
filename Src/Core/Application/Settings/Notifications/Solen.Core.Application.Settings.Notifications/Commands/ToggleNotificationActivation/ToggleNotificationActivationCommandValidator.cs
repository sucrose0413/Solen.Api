using FluentValidation;

namespace Solen.Core.Application.Settings.Notifications.Commands
{
    public class ToggleNotificationActivationCommandValidator : AbstractValidator<ToggleNotificationActivationCommand>
    {
        public ToggleNotificationActivationCommandValidator()
        {
            RuleFor(x => x.TemplateId).NotEmpty();
        }
    }
}