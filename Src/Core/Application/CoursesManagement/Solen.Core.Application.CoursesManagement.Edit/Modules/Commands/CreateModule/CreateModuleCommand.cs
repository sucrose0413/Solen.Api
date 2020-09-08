using MediatR;


namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class CreateModuleCommand : IRequest<CommandResponse>
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string CourseId { get; set; }
    }
}
