using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class UpdateCourseService : IUpdateCourseService
    {
        private readonly IUpdateCourseRepository _repo;

        public UpdateCourseService(IUpdateCourseRepository repo)
        {
            _repo = repo;
        }

        public void UpdateCourseTitle(Course course, string title)
        {
            course.UpdateTitle(title);
        }

        public void UpdateCourseSubtitle(Course course, string subtitle)
        {
            course.UpdateSubtitle(subtitle);
        }

        public void UpdateCourseDescription(Course course, string description)
        {
            course.UpdateDescription(description);
        }

        public async Task RemoveCourseSkillsFromRepo(string courseId, CancellationToken token)
        {
            await _repo.RemoveCourseSkills(courseId, token);
        }

        public void AddSkillsToCourse(Course course, List<string> skills)
        {
            skills.ForEach(course.AddLearnedSkill);
        }
    }
}