using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Common.Impl;

namespace Solen.Persistence.CoursesManagement.Common
{
    public class CourseErrorsRepository : INoModuleErrorRepository, INoLectureErrorsRepository,
        INoContentErrorsRepository, INoMediaErrorsRepository
    {
        private readonly SolenDbContext _context;

        public CourseErrorsRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesCourseHaveModules(string courseId, CancellationToken token)
        {
            return await _context.Modules
                .AnyAsync(x => x.CourseId == courseId, token);
        }

        public async Task<IEnumerable<ModuleInErrorDto>> GetModulesWithoutLectures(string courseId,
            CancellationToken token)
        {
            return await _context.Modules
                .Where(x => x.Lectures.Count == 0 && x.CourseId == courseId)
                .Select(x => new ModuleInErrorDto(x.Id, x.Order))
                .ToListAsync(token);
        }

        public async Task<IEnumerable<LectureInErrorDto>> GetArticleLecturesWithoutContent(string courseId,
            CancellationToken token)
        {
            return await _context.ArticleLectures
                .Where(x => x.Content == null && x.Module.CourseId == courseId)
                .Select(x => new LectureInErrorDto(x.Id, x.ModuleId, x.Order, x.Module.Order))
                .ToListAsync(token);
        }

        public async Task<IEnumerable<LectureInErrorDto>> GetMediaLecturesWithoutUrl(string courseId,
            CancellationToken token)
        {
            return await _context.VideoLectures
                .Where(x => x.Url == null && x.Module.CourseId == courseId)
                .Select(x => new LectureInErrorDto(x.Id, x.ModuleId, x.Order, x.Module.Order))
                .ToListAsync(token);
        }
    }
}