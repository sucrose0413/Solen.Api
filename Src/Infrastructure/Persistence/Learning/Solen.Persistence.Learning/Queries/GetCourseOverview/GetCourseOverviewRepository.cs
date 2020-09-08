using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Learning.Queries
{
    public class GetCourseOverviewRepository : IGetCourseOverviewRepository
    {
        private readonly SolenDbContext _context;

        public GetCourseOverviewRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearnerCourseOverviewDto> GetCourseOverview(string courseId, string learningPathId,
            string publishedStatus, CancellationToken token)
        {
            return await _context.Courses
                .Where(x => x.Id == courseId && x.CourseStatusName == publishedStatus &&
                            x.CourseLearningPaths.Any(lp => lp.LearningPathId == learningPathId))
                .Select(CourseMapping())
                .FirstOrDefaultAsync(token);
        }

        #region Mapping

        private static Expression<Func<CourseLearnedSkill, LearnerCourseLearnedSkillDto>> LearnedSkillMapping()
        {
            return x => new LearnerCourseLearnedSkillDto
            {
                Id = x.Id,
                Name = x.Name
            };
        }

        private static Expression<Func<Lecture, LearnerLectureOverviewDto>> LectureMapping()
        {
            return x => new LearnerLectureOverviewDto
            {
                Id = x.Id,
                Title = x.Title,
                Duration = x.Duration,
                Order = x.Order,
                LectureType = x.LectureType.Name
            };
        }

        private static Expression<Func<Module, LearnerModuleOverviewDto>> ModuleMapping()
        {
            return x => new LearnerModuleOverviewDto
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                Duration = x.Lectures.Sum(l => l.Duration),
                Lectures = x.Lectures.OrderBy(l =>l.Order).AsQueryable().Select(LectureMapping())
            };
        }

        private static Expression<Func<Course, LearnerCourseOverviewDto>> CourseMapping()
        {
            return x => new LearnerCourseOverviewDto
            {
                Id = x.Id,
                Title = x.Title,
                Duration = x.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                Creator = x.Creator.UserName,
                Modules = x.Modules.OrderBy(m => m.Order).AsQueryable().Select(ModuleMapping()),
                CreationDate = x.CreationDate,
                CourseLearnedSkills = x.CourseLearnedSkills.AsQueryable().Select(LearnedSkillMapping()),
                Subtitle = x.Subtitle,
                Description = x.Description,
                LectureCount = x.Modules.SelectMany(m => m.Lectures).Count()
            };
        }

        #endregion
    }
}