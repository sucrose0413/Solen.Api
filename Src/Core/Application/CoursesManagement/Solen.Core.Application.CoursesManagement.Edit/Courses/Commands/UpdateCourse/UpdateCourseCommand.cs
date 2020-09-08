using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateCourseCommand : IRequest
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<string> CourseLearnedSkills { get; set; } = new List<string>();
    }
}
