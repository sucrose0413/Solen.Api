namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class StatusFilterDto
    {
        public StatusFilterDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}