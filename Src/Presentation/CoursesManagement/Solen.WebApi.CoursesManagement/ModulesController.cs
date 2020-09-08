using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.CoursesManagement.Modules.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/modules")]
    [SwaggerTag("(Courses Management)")]
    public class ModulesController : BaseController
    {
        [HttpGet("{moduleId}")]
        public async Task<ActionResult<ModuleViewModel>> GetModule(string moduleId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetModuleQuery {ModuleId = moduleId}, token));
        }
    }
}