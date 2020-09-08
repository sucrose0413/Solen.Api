namespace Solen.Core.Application.LearningPaths.Queries
{
    public class LearningPathDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseCount { get; set; }
        public int LearnerCount { get; set; }
        public bool IsDeletable { get; set; }
    }
}