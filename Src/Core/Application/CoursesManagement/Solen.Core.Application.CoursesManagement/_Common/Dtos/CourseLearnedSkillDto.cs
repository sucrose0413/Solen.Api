namespace Solen.Core.Application.CoursesManagement.Common
{
    public class CourseLearnedSkillDto
    {
        public CourseLearnedSkillDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}