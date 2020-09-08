using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Domain.Courses.Entities;


namespace Solen.Persistence.CoursesManagement.Courses
{
    public class GetCourseContentRepository : IGetCourseContentRepository
    {
        private readonly SolenDbContext _context;

        public GetCourseContentRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<CourseContentDto> GetCourseContent(string courseId, string organizationId,
            CancellationToken token)
        {
            var course = await _context.Courses
                .Include(c => c.Creator)
                .Include(c => c.CourseLearningPaths)
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lectures)
                .Where(c => c.Id == courseId && c.Creator.OrganizationId == organizationId)
                .AsNoTracking()
                .SingleOrDefaultAsync(token);

            return course == null ? null : MapCourseContent(course);
        }

        #region Private Methods

        private static CourseContentDto MapCourseContent(Course course)
        {
            var courseContent = new CourseContentDto
            {
                CourseId = course.Id,
                Modules = course.Modules.Select(MapModules()).OrderBy(m => m.ModuleInfo.Order).ToList()
            };
            return courseContent;
        }

        private static ModuleDto MapModule(Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Name = module.Name,
                Order = module.Order,
                Duration = module.Lectures.Sum(l => l.Duration)
            };
        }

        private static Func<Module, ModuleDetailDto> MapModules()
        {
            return x => new ModuleDetailDto
            {
                ModuleInfo = MapModule(x),
                Lectures = x.Lectures.Select(MapLectures()).OrderBy(l => l.Order).ToList()
            };
        }

        private static Func<Lecture, LectureDto> MapLectures()
        {
            return x => new LectureDto
            {
                Id = x.Id,
                Title = x.Title,
                LectureType = x.LectureTypeName,
                ModuleId = x.ModuleId,
                Content = !x.LectureType.IsMediaLecture ? ((ArticleLecture) x).Content : null,
                VideoUrl = x.LectureType.IsMediaLecture ? ((MediaLecture) x).Url : null,
                Duration = x.Duration,
                Order = x.Order
            };
        }

        #endregion
    }
}