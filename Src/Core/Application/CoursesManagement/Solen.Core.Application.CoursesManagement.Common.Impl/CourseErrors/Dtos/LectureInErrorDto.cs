namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class LectureInErrorDto
    {
        public LectureInErrorDto(string lectureId, string moduleId, int order, int moduleOrder)
        {
            LectureId = lectureId;
            ModuleId = moduleId;
            Order = order;
            ModuleOrder = moduleOrder;
        }

        public string LectureId { get; }
        public string ModuleId { get; }
        public int Order { get; }
        public int ModuleOrder { get; }
    }
}