using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Dashboard.Services.Queries;

namespace Solen.Persistence.Dashboard.Queries
{
    public class GetCoursesInfoRepository : IGetCoursesInfoRepository
    {
        private readonly SolenDbContext _context;

        public GetCoursesInfoRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LastCreatedCourseDto> GetLastCreatedCourse(string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .Where(x => x.Creator.OrganizationId == organizationId)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new LastCreatedCourseDto
                {
                    CourseId = x.Id,
                    CourseCreator = x.Creator.UserName,
                    CourseTitle = x.Title,
                    CreationDate = x.CreationDate
                }).FirstOrDefaultAsync(token);
        }

        public async Task<LastPublishedCourseDto> GetLastPublishedCourse(string organizationId, string publishedStatus, CancellationToken token)
        {
            return await _context.Courses
                .Where(x => x.Creator.OrganizationId == organizationId &&
                            x.CourseStatusName == publishedStatus)
                .OrderByDescending(x => x.PublicationDate)
                .Select(x => new LastPublishedCourseDto
                {
                    CourseId = x.Id,
                    CoursePublisher = x.LastModifiedBy,
                    CourseTitle = x.Title,
                    PublicationDate = x.PublicationDate
                }).FirstOrDefaultAsync(token);
        }

        public async Task<int> GetCourseCount(string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .CountAsync(x => x.Creator.OrganizationId == organizationId, token);
        }
    }
}