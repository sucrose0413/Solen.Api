using System.Collections.Generic;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.LearningPaths.Queries
{
    public class GetCourseLearningPathsListVm
    {
        public IList<string> CourseLearningPathsIds { get; set; }
        public IList<CourseLearningPathDto> LearningPaths { get; set; }
    }
}