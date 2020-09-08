using System.Collections.Generic;

namespace Solen.Core.Application.CoursesManagement.Common
{
    public class CourseContentDto
    {
        public string CourseId { get; set; }
        public IList<ModuleDetailDto> Modules { get; set; }
    }
}