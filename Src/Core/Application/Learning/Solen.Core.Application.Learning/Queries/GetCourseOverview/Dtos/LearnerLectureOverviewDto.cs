namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerLectureOverviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public string LectureType { get; set; }
    }
}