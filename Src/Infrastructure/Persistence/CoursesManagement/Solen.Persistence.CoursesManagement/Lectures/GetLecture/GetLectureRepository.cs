using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Lectures
{
    public class GetLectureRepository : IGetLectureRepository
    {
        private readonly SolenDbContext _context;

        public GetLectureRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LectureDto> GetLecture(string lectureId, string organizationId, CancellationToken token)
        {
            return await _context.Lectures
                .Where(l => l.Id == lectureId && l.Module.Course.Creator.OrganizationId == organizationId)
                .Select(MapLecture())
                .SingleOrDefaultAsync(token);
        }

        #region Private Methods

        private static Expression<Func<Lecture, LectureDto>> MapLecture()
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