namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoMediaError : CourseError
    {
        public NoMediaError() : base(4, "The lecture has no media")
        {
        }
    }
}