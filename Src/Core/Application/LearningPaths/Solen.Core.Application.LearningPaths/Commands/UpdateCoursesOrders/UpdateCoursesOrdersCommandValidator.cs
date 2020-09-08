using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateCoursesOrdersCommandValidator : AbstractValidator<UpdateCoursesOrdersCommand>
    {
        public UpdateCoursesOrdersCommandValidator()
        {
            RuleFor(x => x.CoursesOrders).NotEmpty();
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}