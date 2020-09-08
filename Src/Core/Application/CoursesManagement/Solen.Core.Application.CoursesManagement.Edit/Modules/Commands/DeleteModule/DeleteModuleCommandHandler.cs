using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, CommandResponse>
    {
        private readonly IDeleteModuleService _service;
        private readonly IModulesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteModuleCommandHandler(IDeleteModuleService service, IModulesCommonService commonService,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<CommandResponse> Handle(DeleteModuleCommand command, CancellationToken token)
        {
            var moduleToDelete = await _commonService.GetModuleFromRepo(command.ModuleId, token);

            _commonService.CheckCourseStatusForModification(moduleToDelete);

            _service.RemoveModuleFromRepo(moduleToDelete);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new ModuleDeletedEvent(moduleToDelete.Id), token);

            return new CommandResponse {Value = moduleToDelete.Id};
        }
    }
}