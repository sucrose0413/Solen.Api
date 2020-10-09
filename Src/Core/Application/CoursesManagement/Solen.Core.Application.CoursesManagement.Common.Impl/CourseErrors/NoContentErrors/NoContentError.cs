namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoContentError : CourseError
    {
        public static readonly NoContentError Instance = new NoContentError();
        public NoContentError() : base(3, "The article has no content")
        {
        }
    }
}