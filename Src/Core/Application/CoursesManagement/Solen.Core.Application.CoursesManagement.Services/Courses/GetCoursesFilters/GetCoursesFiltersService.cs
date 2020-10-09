using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class GetCoursesFiltersService : IGetCoursesFiltersService
    {
        private readonly IGetCoursesFiltersRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCoursesFiltersService(IGetCoursesFiltersRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<CoursesManagementAuthorFilterDto>> GetCoursesAuthors(CancellationToken token)
        {
            return await _repo.GetCoursesAuthors(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<IList<LearningPathFilterDto>> GetLearningPaths(CancellationToken token)
        {
            return await _repo.GetLearningPaths(_currentUserAccessor.OrganizationId, token);
        }

        public IList<CoursesManagementOrderByDto> GetOrderByValues()
        {
            var orderByList = Enumeration.GetAll<CourseOrderBy>()
                .Select(x => new CoursesManagementOrderByDto(x.Value, x.Name)).ToList();

            return orderByList;
        }

        public int GetOrderByDefaultValue()
        {
            return OrderByCreationDateDesc.Instance.Value;
        }

        public IList<StatusFilterDto> GetStatus()
        {
            var statusList = Enumeration.GetAll<CourseStatus>()
                .Select(x => new StatusFilterDto(x.Value, x.Name)).ToList();

            return statusList;
        }
    }
}