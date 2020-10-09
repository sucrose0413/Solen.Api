namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class OrderByAuthor : CourseOrderBy
    {
        public static readonly OrderByAuthor Instance = new OrderByAuthor();
        public OrderByAuthor() : base(1, "Author")
        {
        }

        public override bool IsSortDescending => false;
    }
}