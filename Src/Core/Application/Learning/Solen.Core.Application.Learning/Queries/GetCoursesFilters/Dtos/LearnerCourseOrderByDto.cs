namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseOrderByDto
    {
        public LearnerCourseOrderByDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}