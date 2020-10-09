using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.Learning.Services.Queries
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

        public async Task<LearnerCoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery,
            CancellationToken token)
        {
            if (coursesQuery.OrderBy == 0)
                coursesQuery.OrderBy = OrderByLastAccessed.Instance.Value;

            return await _repo.GetCoursesList(coursesQuery, _currentUserAccessor.UserId,
                _currentUserAccessor.LearningPathId, PublishedStatus.Instance.Name, token);
        }
    }
}