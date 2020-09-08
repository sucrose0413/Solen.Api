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
    public class GetCourseContentRepository : IGetCourseContentRepository
    {
        private readonly SolenDbContext _context;

        public GetCourseContentRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearnerCourseContentDto> GetCourseContentFromRepo(string courseId, string learningPathId,
            string publishedStatus, CancellationToken token)
        {
            return await _context.Courses
                .Where(CourseCriteria(courseId, learningPathId, publishedStatus))
                .Select(MapCourseContent())
                .SingleOrDefaultAsync(token);
        }

        public async Task<string> GetLastLectureId(string courseId, string learnerId, CancellationToken token)
        {
            return await _context.LearnerLectureAccessHistories.Where(x =>
                    x.LearnerId == learnerId && x.Lecture.Module.CourseId == courseId)
                .OrderByDescending(x => x.AccessDate)
                .Select(x => x.LectureId)
                .FirstOrDefaultAsync(token);
        }


        #region Private Methods

        private static Expression<Func<Course, bool>> CourseCriteria(string courseId, string learningPathId, string publishedStatus)
        {
            return x =>
                x.Id == courseId && x.CourseStatusName == publishedStatus &&
                x.CourseLearningPaths.Any(l => l.LearningPathId == learningPathId);
        }

        private static Expression<Func<Course, LearnerCourseContentDto>> MapCourseContent()
        {
            return x => new LearnerCourseContentDto
            {
                Title = x.Title,
                CourseId = x.Id,
                Duration = x.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                Modules = x.Modules.AsQueryable().Select(MapModule()).OrderBy(m => m.Order).ToList()
            };
        }

        private static Expression<Func<Module, LearnerModuleDto>> MapModule()
        {
            return x => new LearnerModuleDto
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                Duration = x.Lectures.Sum(l => l.Duration),
                Lectures = x.Lectures.AsQueryable().Select(MapLectures()).OrderBy(l => l.Order).ToList()
            };
        }


        private static Expression<Func<Lecture, LearnerLectureDto>> MapLectures()
        {
            return x => new LearnerLectureDto
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