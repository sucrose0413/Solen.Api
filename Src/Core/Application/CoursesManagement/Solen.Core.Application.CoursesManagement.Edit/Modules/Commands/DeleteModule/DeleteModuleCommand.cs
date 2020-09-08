using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class DeleteModuleCommand : IRequest<CommandResponse>
    {
        public string ModuleId { get; set; }
    }
}
