using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Courses
{
    public class DeleteCourseResources : INotificationHandler<CourseDeletedEvent>
    {
        private readonly ICourseResourcesRepo _repo;
        private readonly IAppResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseResources(ICourseResourcesRepo repo, IAppResourceManager resourceManager,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _resourceManager = resourceManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CourseDeletedEvent @event, CancellationToken token)
        {
            var resourcesToDelete = await _repo.GetCourseResources(@event.CourseId, token);

            foreach (var resource in resourcesToDelete)
                await _resourceManager.Delete(resource, token);

            await _unitOfWork.SaveAsync(token);
        }
    }
}