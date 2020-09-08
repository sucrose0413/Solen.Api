using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Modules
{
    public class DeleteModuleResources : INotificationHandler<ModuleDeletedEvent>
    {
        private readonly IModuleResourcesRepo _repo;
        private readonly IAppResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModuleResources(IModuleResourcesRepo repo, IAppResourceManager resourceManager,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _resourceManager = resourceManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ModuleDeletedEvent @event, CancellationToken token)
        {
            var resourcesToDelete = await _repo.GetModuleResources(@event.ModuleId, token);

            foreach (var resource in resourcesToDelete)
                await _resourceManager.Delete(resource, token);

            await _unitOfWork.SaveAsync(token);
        }
    }
}