namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class OrderByAuthor : CourseOrderBy
    {
        public OrderByAuthor() : base(1, "Author")
        {
        }

        public override bool IsSortDescending => false;
    }
}