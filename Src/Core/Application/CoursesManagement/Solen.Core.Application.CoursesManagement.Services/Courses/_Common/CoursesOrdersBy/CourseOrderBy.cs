using Solen.Core.Domain.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public abstract class CourseOrderBy : Enumeration
    {
        protected CourseOrderBy(int value, string name) : base(value, name)
        {
        }

        public abstract bool IsSortDescending { get; }
    }
}