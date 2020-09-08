namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class OrderByCreationDateDesc : CourseOrderBy
    {
        public OrderByCreationDateDesc() : base(4, "Creation Date desc")
        {
        }

        public override bool IsSortDescending => true;
    }
}