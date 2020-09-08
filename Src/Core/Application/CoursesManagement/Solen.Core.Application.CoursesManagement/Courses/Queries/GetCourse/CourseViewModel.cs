using System.Collections.Generic;
using Solen.Core.Application.CoursesManagement.Common;


namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CourseViewModel
    {
        public CourseDto Course { get; set; }
        public IEnumerable<CourseErrorDto> CourseErrors { get; set; }
    }
}