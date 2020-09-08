using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Application.SigningUp.Organizations.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.SigningUp
{
    [AllowAnonymous]
    [Route("api/signing-up/organizations")]
    [SwaggerTag("(Signing Up)")]
    public class OrganizationsController : BaseController
    {
        [HttpPost("init")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> InitSigningUp(InitSigningUpCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPost("complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CompleteSigningUp(CompleteOrganizationSigningUpCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPost("check")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CheckToken(CheckOrganizationSigningUpTokenQuery query, CancellationToken token)
        {
            await Mediator.Send(query, token);

            return NoContent();
        }
    }
}