using FluentValidation;

namespace Solen.Core.Application.Settings.Organization.Commands
{
    public class UpdateOrganizationInfoCommandValidator : AbstractValidator<UpdateOrganizationInfoCommand>
    {
        public UpdateOrganizationInfoCommandValidator()
        {
            RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(60);
        }
    }
}