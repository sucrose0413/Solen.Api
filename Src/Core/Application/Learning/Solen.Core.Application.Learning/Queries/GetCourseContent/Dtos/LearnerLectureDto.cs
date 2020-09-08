namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerLectureDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public string LectureType { get; set; }
        public string ModuleId { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; }
    }
}