namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class NoContentError : CourseError
    {
        public NoContentError() : base(3, "The article has no content")
        {
        }
    }
}