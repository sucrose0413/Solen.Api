

using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class CourseResource
    {
        #region Constructors

        private CourseResource()
        {
        }

        public CourseResource(string courseId, string moduleId, string lectureId, string resourceId)
        {
            CourseId = courseId;
            ModuleId = moduleId;
            LectureId = lectureId;
            ResourceId = resourceId;
        }

        #endregion

        #region Public Properties

        public string CourseId { get; private set; }
        public string ModuleId { get; private set; }
        public string LectureId { get; private set; }
        public string ResourceId { get; private set; }
        public AppResource Resource { get; private set; }

        #endregion
    }
}