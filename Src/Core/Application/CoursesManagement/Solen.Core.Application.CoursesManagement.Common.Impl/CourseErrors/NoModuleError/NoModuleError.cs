namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoModuleError : CourseError
    {
        public static readonly NoModuleError Instance = new NoModuleError();
        public NoModuleError() : base(1, "The course has no module")
        {
        }
    }
}