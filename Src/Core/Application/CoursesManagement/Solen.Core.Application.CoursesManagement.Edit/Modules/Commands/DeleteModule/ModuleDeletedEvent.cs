using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public class ModuleDeletedEvent : INotification
    {
        public ModuleDeletedEvent(string moduleId)
        {
            ModuleId = moduleId;
        }

        public string ModuleId { get; }
    }
}