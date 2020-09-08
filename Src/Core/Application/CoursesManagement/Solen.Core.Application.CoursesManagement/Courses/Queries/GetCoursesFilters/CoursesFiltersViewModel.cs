using System.Collections.Generic;


namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CoursesFiltersViewModel
    {
        public IList<CoursesManagementOrderByDto> OrderByFiltersList { get; set; }
        public int OrderByDefaultValue { get; set; }
        public IList<CoursesManagementAuthorFilterDto> AuthorsFiltersList { get; set; }
        public IList<LearningPathFilterDto> LearningPathsFiltersList { get; set; }
        public IList<StatusFilterDto> StatusFiltersList { get; set; }
    }
}