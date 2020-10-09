namespace Solen.Core.Domain.Courses.Entities
{
    public class ArticleLecture : Lecture
    {
        #region Constructors

        public ArticleLecture(string title, string moduleId, int order, string content) : base(title, moduleId,
            Enums.LectureTypes.ArticleLecture.Instance, order)
        {
            Content = content;
        }

        public ArticleLecture(string title, Module module, int order, string content) : base(title, module,
            Enums.LectureTypes.ArticleLecture.Instance, order)
        {
            Content = content;
        }

        #endregion

        #region Public Properties & Methods

        public string Content { get; private set; }

        public void UpdateContent(string content)
        {
            Content = content;
        }

        #endregion
    }
}