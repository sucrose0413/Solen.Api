using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Modules.Queries;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Modules
{
    public class GetModuleService : IGetModuleService
    {
        private readonly IGetModuleRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetModuleService(IGetModuleRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<ModuleDto> GetModule(string moduleId, CancellationToken token)
        {
            return await _repo.GetModule(moduleId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The module ({moduleId}) does not exist");
        }
    }
}