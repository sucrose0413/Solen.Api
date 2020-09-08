using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.Common.Security;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class GetCoursesListService : IGetCoursesListService
    {
        private readonly IGetCoursesListRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCoursesListService(IGetCoursesListRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<CoursesListResult> GetCoursesList(GetCoursesListQuery query, CancellationToken token)
        {
            if (query.OrderBy == 0)
                query.OrderBy = new OrderByCreationDateDesc().Value;

            return await _repo.GetCoursesList(query, _currentUserAccessor.OrganizationId, token);
        }
    }
}