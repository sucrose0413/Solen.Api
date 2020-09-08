using Solen.Core.Domain.Common;

namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public abstract class CourseError : Enumeration
    {
        protected CourseError(int value, string name) : base(value, name)
        {
        }
    }
}