namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class OrderByAuthorDesc : CourseOrderBy
    {
        public static readonly OrderByAuthorDesc Instance = new OrderByAuthorDesc();
        public OrderByAuthorDesc() : base(2, "Author desc")
        {
        }

        public override bool IsSortDescending => true;
    }
}