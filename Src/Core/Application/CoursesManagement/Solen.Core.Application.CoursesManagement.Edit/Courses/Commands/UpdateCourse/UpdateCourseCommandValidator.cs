using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(60).NotEmpty();
            RuleFor(x => x.Subtitle).MaximumLength(120);
            RuleForEach(x => x.CourseLearnedSkills).Must(s => s.Length <= 150)
                .WithMessage("The length of a skill must be 150 characters or fewer.");
        }
    }
}