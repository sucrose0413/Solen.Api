namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoMediaError : CourseError
    {
        public static readonly NoMediaError Instance = new NoMediaError();
        public NoMediaError() : base(4, "The lecture has no media")
        {
        }
    }
}