using System;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class LastCreatedCourseDto
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCreator { get; set; }
        public DateTime CreationDate { get; set; }
    }
}