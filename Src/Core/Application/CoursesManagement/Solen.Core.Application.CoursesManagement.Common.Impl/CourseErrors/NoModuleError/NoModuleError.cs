namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoModuleError : CourseError
    {
        public NoModuleError() : base(1, "The course has no module")
        {
        }
    }
}