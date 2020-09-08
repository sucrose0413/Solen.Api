namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseAuthorFilterDto
    {
        public LearnerCourseAuthorFilterDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}