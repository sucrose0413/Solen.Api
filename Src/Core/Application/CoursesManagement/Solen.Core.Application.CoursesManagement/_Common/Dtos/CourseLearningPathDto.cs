namespace Solen.Core.Application.CoursesManagement.Common
{
    public class CourseLearningPathDto
    {
        public CourseLearningPathDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}