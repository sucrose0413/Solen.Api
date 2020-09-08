using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateLecturesOrdersCommandValidator : AbstractValidator<UpdateLecturesOrdersCommand>
    {
        public UpdateLecturesOrdersCommandValidator()
        {
            RuleFor(x => x.LecturesOrders).NotEmpty();

            RuleFor(x => x.ModuleId).NotEmpty();
        }
    }
}