namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class OrderByCreationDate : CourseOrderBy
    {
        public OrderByCreationDate() : base(3, "Creation Date")
        {
        }

        public override bool IsSortDescending => false;
    }
}