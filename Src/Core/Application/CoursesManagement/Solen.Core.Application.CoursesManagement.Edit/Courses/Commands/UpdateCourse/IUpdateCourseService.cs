using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface IUpdateCourseService
    {
        void UpdateCourseTitle(Course course, string title);
        void UpdateCourseSubtitle(Course course, string subtitle);
        void UpdateCourseDescription(Course course, string description);
        Task RemoveCourseSkillsFromRepo(string courseId, CancellationToken token);
        void AddSkillsToCourse(Course course, List<string> skills);
    }
}