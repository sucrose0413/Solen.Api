using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Modules.Queries
{
    public class GetModuleQueryHandler : IRequestHandler<GetModuleQuery, ModuleViewModel>
    {
        private readonly IGetModuleService _modulesService;

        public GetModuleQueryHandler(IGetModuleService modulesService)
        {
            _modulesService = modulesService;
        }

        public async Task<ModuleViewModel> Handle(GetModuleQuery query, CancellationToken token)
        {
            var module = await _modulesService.GetModule(query.ModuleId, token);

            return new ModuleViewModel(module);
        }
    }
}