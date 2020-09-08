using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Courses.Enums.CourseStatuses
{
    public abstract class CourseStatus : Enumeration
    {
        protected CourseStatus(int value, string name) : base(value, name)
        {
        }
    }
}