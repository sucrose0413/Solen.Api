namespace Solen.Core.Domain.Courses.Enums.LectureTypes
{
    public class ArticleLecture : LectureType
    {
        public static readonly ArticleLecture Instance = new ArticleLecture();

        private ArticleLecture() : base(1, "Article")
        {
        }
        public override bool IsMediaLecture => false;
    }
}