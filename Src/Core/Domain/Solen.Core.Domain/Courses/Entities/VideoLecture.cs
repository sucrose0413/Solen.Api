
using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Domain.Courses.Entities
{
    public class VideoLecture : MediaLecture
    {
        public VideoLecture(string title, string moduleId, int order) : base(title, moduleId, new Enums.LectureTypes.VideoLecture(), order)
        {
        }
        
        public VideoLecture(string title, Module module, int order) : base(title, module, new Enums.LectureTypes.VideoLecture(), order)
        {
        }
    }
}