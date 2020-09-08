using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Lectures.Queries
{
    public class GetLectureQueryValidator : AbstractValidator<GetLectureQuery>
    {
        public GetLectureQueryValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
        }
    }
}