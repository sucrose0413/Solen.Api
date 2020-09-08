using System.Collections.Generic;


namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCoursesFiltersViewModel
    {
        public IList<LearnerCourseOrderByDto> OrderByFiltersList { get; set; }
        public int OrderByDefaultValue { get; set; }
        public IList<LearnerCourseAuthorFilterDto> AuthorsFiltersList { get; set; }
    }
}