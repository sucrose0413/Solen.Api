namespace Solen.Core.Application.CoursesManagement.Common.Impl
{
    public class ModuleInErrorDto
    {
        public ModuleInErrorDto(string moduleId, int order)
        {
            ModuleId = moduleId;
            Order = order;
        }

        public string ModuleId { get; }
        public int Order { get; }
    }
}