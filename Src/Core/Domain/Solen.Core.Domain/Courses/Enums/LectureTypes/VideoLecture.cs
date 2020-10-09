namespace Solen.Core.Domain.Courses.Enums.LectureTypes
{
    public class VideoLecture : LectureType
    {
        public static readonly VideoLecture Instance = new VideoLecture();
        
        private VideoLecture() : base(2, "Video")
        {
        }
        
        public override bool IsMediaLecture => true;
    }
}