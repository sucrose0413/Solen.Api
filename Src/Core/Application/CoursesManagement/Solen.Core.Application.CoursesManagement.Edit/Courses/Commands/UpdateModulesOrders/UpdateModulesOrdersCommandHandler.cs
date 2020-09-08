using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateModulesOrdersCommandHandler : IRequestHandler<UpdateModulesOrdersCommand, Unit>
    {
        private readonly IUpdateModulesOrdersService _service;
        private readonly ICoursesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateModulesOrdersCommandHandler(IUpdateModulesOrdersService service,
            ICoursesCommonService commonService, IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateModulesOrdersCommand command, CancellationToken token)
        {
            var course = await _commonService.GetCourseFromRepo(command.CourseId, token);

            _commonService.CheckCourseStatusForModification(course);

            var modulesToUpdateOrders = await _service.GetCourseModulesFromRepo(command.CourseId, token);

            _service.UpdateModulesOrders(modulesToUpdateOrders, command.ModulesOrders);

            _service.UpdateModulesRepo(modulesToUpdateOrders);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}