using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Domain.Courses.Entities
{
    public abstract class MediaLecture : Lecture
    {
        protected MediaLecture(string title, string moduleId, LectureType lectureType, int order) : base(title,
            moduleId, lectureType, order)
        {
        }
        
        protected MediaLecture(string title, Module module, LectureType lectureType, int order) : base(title,
            module, lectureType, order)
        {
        }

        public string Url { get; private set; }

        public virtual void SetUrl(string url)
        {
            Url = url;
        }
    }
}