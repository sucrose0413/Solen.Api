using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Lectures
{
    public class DeleteLectureResources : INotificationHandler<LectureDeletedEvent>
    {
        private readonly ILectureResourcesRepo _repo;
        private readonly IAppResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLectureResources(ILectureResourcesRepo repo, IAppResourceManager resourceManager,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _resourceManager = resourceManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LectureDeletedEvent @event, CancellationToken token)
        {
            var resourcesToDelete = await _repo.GetLectureResources(@event.LectureId, token);

            foreach (var resource in resourcesToDelete)
                await _resourceManager.Delete(resource, token);

            await _unitOfWork.SaveAsync(token);
        }
    }
}