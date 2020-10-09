namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoLectureError : CourseError
    {
        public static readonly NoLectureError Instance = new NoLectureError();
        public NoLectureError() :base(2, "The module has no lecture")
        {
        }
    }
}