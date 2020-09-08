using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, CommandResponse>
    {
        private readonly ICreateModuleService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CreateModuleCommandHandler(ICreateModuleService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(CreateModuleCommand command, CancellationToken token)
        {
            await _service.ControlCourseExistenceAndStatus(command.CourseId, token);

            var moduleToCreate = _service.CreateModule(command);

            await _service.AddModuleToRepo(moduleToCreate, token);

            await _unitOfWork.SaveAsync(token);

            return new CommandResponse {Value = moduleToCreate.Id};
        }

    }
}