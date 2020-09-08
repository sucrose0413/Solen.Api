using System.Collections.Generic;


namespace Solen.Core.Application.CoursesManagement.Common
{
    public class ModuleDetailDto
    {
        public ModuleDto ModuleInfo { get; set; }

        public IList<LectureDto> Lectures { get; set; }
    }
}