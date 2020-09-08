namespace Solen.Core.Application.Users.Queries
{
    public class LearningPathForUserDto
    {
        public LearningPathForUserDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}