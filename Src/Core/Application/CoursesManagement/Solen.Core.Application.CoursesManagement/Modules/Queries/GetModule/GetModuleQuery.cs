using MediatR;

namespace Solen.Core.Application.CoursesManagement.Modules.Queries
{
    public class GetModuleQuery : IRequest<ModuleViewModel>
    {
        public string ModuleId { get; set; }
    }
}
