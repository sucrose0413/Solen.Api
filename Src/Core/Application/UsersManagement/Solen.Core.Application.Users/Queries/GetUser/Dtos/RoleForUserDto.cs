namespace Solen.Core.Application.Users.Queries
{
    public class RoleForUserDto
    {
        public RoleForUserDto(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}