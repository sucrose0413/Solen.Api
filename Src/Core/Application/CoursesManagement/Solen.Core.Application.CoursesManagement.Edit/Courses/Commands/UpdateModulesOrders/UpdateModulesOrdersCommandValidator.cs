using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateModulesOrdersCommandValidator : AbstractValidator<UpdateModulesOrdersCommand>
    {
        public UpdateModulesOrdersCommandValidator()
        {
            RuleFor(x => x.ModulesOrders).NotEmpty();
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}