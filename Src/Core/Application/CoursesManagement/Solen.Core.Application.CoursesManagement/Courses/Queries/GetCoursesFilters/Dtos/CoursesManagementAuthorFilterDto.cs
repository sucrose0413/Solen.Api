namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CoursesManagementAuthorFilterDto
    {
        public CoursesManagementAuthorFilterDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}