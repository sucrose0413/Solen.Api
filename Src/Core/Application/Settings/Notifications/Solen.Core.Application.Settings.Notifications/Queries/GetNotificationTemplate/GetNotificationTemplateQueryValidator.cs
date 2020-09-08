using FluentValidation;

namespace Solen.Core.Application.Settings.Notifications.Queries
{
    public class GetNotificationTemplateQueryValidator : AbstractValidator<GetNotificationTemplateQuery>
    {
        public GetNotificationTemplateQueryValidator()
        {
            RuleFor(x => x.TemplateId).NotEmpty();
        }
    }
}