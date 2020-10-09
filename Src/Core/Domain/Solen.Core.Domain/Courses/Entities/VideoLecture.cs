namespace Solen.Core.Domain.Courses.Entities
{
    public class VideoLecture : MediaLecture
    {
        public VideoLecture(string title, string moduleId, int order) : base(title, moduleId,
            Enums.LectureTypes.VideoLecture.Instance, order)
        {
        }

        public VideoLecture(string title, Module module, int order) : base(title, module,
            Enums.LectureTypes.VideoLecture.Instance, order)
        {
        }
    }
}