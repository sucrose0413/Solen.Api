using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, Unit>
    {
        private readonly IUpdateModuleService _service;
        private readonly IModulesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateModuleCommandHandler(IUpdateModuleService service, IModulesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateModuleCommand command, CancellationToken token)
        {
            var moduleToUpdate = await _commonService.GetModuleFromRepo(command.ModuleId, token);

            _commonService.CheckCourseStatusForModification(moduleToUpdate);

            _service.UpdateModuleName(moduleToUpdate, command.Name);

            _service.UpdateModuleRepo(moduleToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}