namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class LearningPathFilterDto
    {
        public LearningPathFilterDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}