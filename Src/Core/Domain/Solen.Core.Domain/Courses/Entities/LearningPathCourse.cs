namespace Solen.Core.Domain.Courses.Entities
{
    public class LearningPathCourse
    {
        #region Constructors

        private LearningPathCourse()
        {
        }

        public LearningPathCourse(string learningPathId, string courseId, int order)
        {
            LearningPathId = learningPathId;
            CourseId = courseId;
            Order = order;
        }

        #endregion

        #region Public Properties

        public string LearningPathId { get; private set; }
        public LearningPath LearningPath { get; private set; }
        public string CourseId { get; private set; }
        public Course Course { get; private set; }
        public int Order { get; private set; }

        #endregion

        #region Public Methods

        public void UpdateOrder(int order)
        {
            Order = order;
        }

        #endregion
    }
}