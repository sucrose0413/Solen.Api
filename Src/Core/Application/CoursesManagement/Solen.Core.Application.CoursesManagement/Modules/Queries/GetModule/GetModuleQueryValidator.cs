using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Modules.Queries
{
    public class GetModuleQueryValidator : AbstractValidator<GetModuleQuery>
    {
        public GetModuleQueryValidator()
        {
            RuleFor(x => x.ModuleId).NotEmpty();
        }
    }
}