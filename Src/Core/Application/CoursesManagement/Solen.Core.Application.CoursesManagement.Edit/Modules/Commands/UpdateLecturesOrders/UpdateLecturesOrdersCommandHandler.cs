using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateLecturesOrdersCommandHandler : IRequestHandler<UpdateLecturesOrdersCommand, Unit>
    {
        private readonly IUpdateLecturesOrdersService _service;
        private readonly IModulesCommonService _commonService;

        private readonly IUnitOfWork _unitOfWork;


        public UpdateLecturesOrdersCommandHandler(IUpdateLecturesOrdersService service,
            IModulesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLecturesOrdersCommand command, CancellationToken token)
        {
            var module = await _commonService.GetModuleFromRepo(command.ModuleId, token);

            _commonService.CheckCourseStatusForModification(module);

            var lecturesToUpdateOrders = await _service.GetModuleLecturesFromRepo(command.ModuleId, token);

            _service.UpdateLecturesOrders(lecturesToUpdateOrders, command.LecturesOrders);

            _service.UpdateLecturesRepo(lecturesToUpdateOrders);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}