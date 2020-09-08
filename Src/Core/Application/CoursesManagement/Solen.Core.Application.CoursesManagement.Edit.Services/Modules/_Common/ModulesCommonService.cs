using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public class ModulesCommonService : IModulesCommonService
    {
        private readonly IModulesCommonRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public ModulesCommonService(IModulesCommonRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Module> GetModuleFromRepo(string moduleId, CancellationToken token)
        {
            return await _repo.GetModuleWithCourse(moduleId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException(typeof(Module).Name, moduleId);
        }
        
        public void CheckCourseStatusForModification(Module module)
        {
            if (!module.Course.IsEditable)
                throw new UnalterableCourseException();
        }
    }
}