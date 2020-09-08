namespace Solen.Core.Domain.Courses.Entities
{
    public class CourseLearnedSkill
    {
        #region Constructors

        private CourseLearnedSkill()
        {
        }

        public CourseLearnedSkill(string courseId, string name)
        {
            CourseId = courseId;
            Name = name;
        }

        #endregion

        #region Public Properties

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CourseId { get; private set; }
        public Course Course { get; private set; }

        #endregion
    }
}