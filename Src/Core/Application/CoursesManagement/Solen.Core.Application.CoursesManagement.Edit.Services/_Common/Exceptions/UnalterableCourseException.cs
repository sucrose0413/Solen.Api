using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions
{
    public class UnalterableCourseException : AppBusinessException
    {
        public UnalterableCourseException()
            : base("The Course should be unpublished to modify it")
        {
        }
    }
}