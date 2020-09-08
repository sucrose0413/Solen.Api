using System.Collections.Generic;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class LearningPathCoursesViewModel
    { 
        public IEnumerable<CourseForLearningPathDto> Courses { get; set; }
    }
}