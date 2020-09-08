namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoLectureError : CourseError
    {
        public NoLectureError() :base(2, "The module has no lecture")
        {
        }
    }
}