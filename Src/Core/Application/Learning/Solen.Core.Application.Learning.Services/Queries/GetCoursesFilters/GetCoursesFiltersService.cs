using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Domain.Common;

namespace Solen.Core.Application.Learning.Services.Queries
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

        public async Task<IList<LearnerCourseAuthorFilterDto>> GetCoursesAuthors(CancellationToken token)
        {
            return await _repo.GetCoursesAuthors(_currentUserAccessor.OrganizationId, token);
        }

        public IList<LearnerCourseOrderByDto> GetOrderByValues()
        {
            var orderByList = Enumeration.GetAll<CourseOrderBy>()
                .Select(x => new LearnerCourseOrderByDto(x.Value, x.Name)).ToList();

            return orderByList;
        }

        public int GetOrderByDefaultValue()
        {
            return OrderByLastAccessed.Instance.Value;
        }
    }
}