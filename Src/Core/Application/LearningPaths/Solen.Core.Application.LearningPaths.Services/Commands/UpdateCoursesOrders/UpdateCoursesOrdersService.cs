using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class UpdateCoursesOrdersService : IUpdateCoursesOrdersService
    {
        private readonly IUpdateCoursesOrdersRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateCoursesOrdersService(IUpdateCoursesOrdersRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<List<LearningPathCourse>> GetLearningPathCourses(string learningPathId,
            CancellationToken token)
        {
            return await _repo.GetLearningPathCourses(learningPathId, _currentUserAccessor.OrganizationId, token);
        }

        public void UpdateCoursesOrders(List<LearningPathCourse> courses, IEnumerable<CourseOrderDto> coursesNewOrders)
        {
            var coursesOrdersArray = coursesNewOrders as CourseOrderDto[] ?? coursesNewOrders.ToArray();

            courses.ForEach(x =>
            {
                var order = coursesOrdersArray.Any(m => m.CourseId == x.CourseId)
                    ? coursesOrdersArray.First(m => m.CourseId == x.CourseId).Order
                    : x.Order;

                x.UpdateOrder(order);
            });
        }

        public void UpdateLearningPathCoursesRepo(IEnumerable<LearningPathCourse> courses)
        {
            _repo.UpdateLearningPathCourses(courses);
        }
    }
}