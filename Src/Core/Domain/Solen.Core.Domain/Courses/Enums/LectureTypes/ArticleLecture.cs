namespace Solen.Core.Domain.Courses.Enums.LectureTypes
{
    public class ArticleLecture : LectureType
    {
        public override bool IsMediaLecture => false;

        public ArticleLecture() : base(1, "Article")
        {
        }
    }
}