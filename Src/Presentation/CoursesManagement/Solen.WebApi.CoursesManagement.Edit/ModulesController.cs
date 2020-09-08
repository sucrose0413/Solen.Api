using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement.Edit
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/modules")]
    [SwaggerTag("(Courses Management)")]
    public class ModulesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> CreateModule(CreateModuleCommand command,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateModule(UpdateModuleCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpDelete("{moduleId}")]
        public async Task<ActionResult<CommandResponse>> DeleteModule(string moduleId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new DeleteModuleCommand {ModuleId = moduleId}, token));
        }

        [HttpPut("lecturesOrders")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateModulesOrders(UpdateLecturesOrdersCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}