using System;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class LastPublishedCourseDto
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CoursePublisher { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}