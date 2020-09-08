namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CoursesManagementOrderByDto
    {
        public CoursesManagementOrderByDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}