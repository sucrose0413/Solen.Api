using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}