using System.Collections.Generic;


namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CoursesListResult
    {
        public CoursesListResult(int totalItems, IEnumerable<CoursesListItemDto> items)
        {
            TotalItems = totalItems;
            Items = items ?? new List<CoursesListItemDto>();
        }

        public int TotalItems { get; }

        public IEnumerable<CoursesListItemDto> Items { get; }
    }
}