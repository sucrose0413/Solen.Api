namespace Solen.Core.Application.Learning.Services.Queries
{
    public class OrderByLastAccessed : CourseOrderBy
    {
        public OrderByLastAccessed() : base(1, "Last Accessed")
        {
        }
    }
}