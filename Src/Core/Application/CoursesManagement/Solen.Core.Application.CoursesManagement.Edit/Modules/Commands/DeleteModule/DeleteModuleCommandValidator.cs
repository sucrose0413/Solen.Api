using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
    {
        public DeleteModuleCommandValidator()
        {
            RuleFor(x => x.ModuleId).NotEmpty();
        }
    }
}