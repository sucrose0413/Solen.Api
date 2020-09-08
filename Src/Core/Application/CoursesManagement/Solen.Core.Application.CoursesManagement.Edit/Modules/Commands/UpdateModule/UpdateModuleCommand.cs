using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class UpdateModuleCommand : IRequest
    {
        public string ModuleId { get; set; }
        public string Name { get; set; }
    }
}
