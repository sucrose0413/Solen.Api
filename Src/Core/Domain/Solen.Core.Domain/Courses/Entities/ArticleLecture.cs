using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Domain.Courses.Entities
{
    public class ArticleLecture : Lecture
    {
        #region Constructors

        public ArticleLecture(string title, string moduleId, int order, string content) : base(title, moduleId,
            new Enums.LectureTypes.ArticleLecture(), order)
        {
            Content = content;
        }
        
        public ArticleLecture(string title, Module module, int order, string content) : base(title, module,
            new Enums.LectureTypes.ArticleLecture(), order)
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