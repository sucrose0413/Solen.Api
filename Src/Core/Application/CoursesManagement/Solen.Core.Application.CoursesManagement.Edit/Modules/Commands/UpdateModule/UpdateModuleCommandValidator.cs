using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
    {
        public UpdateModuleCommandValidator()
        {
            RuleFor(x => x.ModuleId).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        }
    }
}