namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class LectureModuleIdCourseId
    {
        public LectureModuleIdCourseId(string moduleId, string courseId)
        {
            ModuleId = moduleId;
            CourseId = courseId;
        }

        public string ModuleId { get; }
        public string CourseId { get; }
    }
}