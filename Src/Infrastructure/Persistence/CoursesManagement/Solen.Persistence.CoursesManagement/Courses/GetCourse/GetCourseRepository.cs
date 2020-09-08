using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Courses
{
    public class GetCourseRepository : IGetCourseRepository
    {
        private readonly SolenDbContext _context;

        public GetCourseRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> GetCourse(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .Where(c => c.Id == courseId && c.Creator.OrganizationId == organizationId)
                .Select(CourseMapping())
                .SingleOrDefaultAsync(token);
        }

        #region Private Methods

        private static Expression<Func<Course, CourseDto>> CourseMapping()
        {
            return course => new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                Creator = course.Creator.UserName,
                Description = course.Description,
                CreationDate = course.CreationDate,
                Status = course.CourseStatus.Name,
                Duration = course.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                IsEditable = course.IsEditable,
                LectureCount = course.Modules.SelectMany(m => m.Lectures).Count(),
                CourseLearnedSkills = course.CourseLearnedSkills.AsQueryable().Select(SkillMapping()).ToList()
                // CourseLearnedSkills = course.CourseLearnedSkills.Select(x => new CourseLearnedSkillDto(x.Id, x.Name)).ToList()
            };
        }

        private static Expression<Func<CourseLearnedSkill, CourseLearnedSkillDto>> SkillMapping()
        {
            return x => new CourseLearnedSkillDto(x.Id, x.Name);
        }

        #endregion
    }
}