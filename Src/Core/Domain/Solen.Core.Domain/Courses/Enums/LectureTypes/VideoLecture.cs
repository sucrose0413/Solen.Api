namespace Solen.Core.Domain.Courses.Enums.LectureTypes
{
    public class VideoLecture : LectureType
    {
        public VideoLecture() : base(2, "Video")
        {
        }

        public override bool IsMediaLecture => true;
    }
}