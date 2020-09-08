using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Settings.Organization.Commands;
using Solen.Core.Application.Settings.Organization.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Settings.Organization
{
    [Authorize(Policy = AuthorizationPolicies.SettingsPolicy)]
    [Route("api/settings/organization")]
    [SwaggerTag("(Organization Settings)")]
    public class OrganizationController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<OrganizationInfoViewModel>> GetTemplates(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetOrganizationInfoQuery(), token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrganizationInfo(UpdateOrganizationInfoCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}