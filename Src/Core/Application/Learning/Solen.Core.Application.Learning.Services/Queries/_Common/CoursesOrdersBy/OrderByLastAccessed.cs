namespace Solen.Core.Application.Learning.Services.Queries
{
    public class OrderByLastAccessed : CourseOrderBy
    {
        public static readonly OrderByLastAccessed Instance = new OrderByLastAccessed();
        public OrderByLastAccessed() : base(1, "Last Accessed")
        {
        }
    }
}