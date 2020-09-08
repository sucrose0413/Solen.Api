using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Modules.Queries
{
    public class ModuleViewModel
    {
        public ModuleViewModel(ModuleDto module)
        {
            Module = module;
        }

        public ModuleDto Module { get; }
    }
}