using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Application.SigningUp.Users.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.SigningUp
{
    [AllowAnonymous]
    [Route("api/signing-up/users")]
    [SwaggerTag("(Signing Up)")]
    public class UsersController : BaseController
    {
        [HttpPost("complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CompleteSigningUp(CompleteUserSigningUpCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
        
        [HttpPost("check")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CheckToken(CheckUserSigningUpTokenQuery query, CancellationToken token)
        {
            await Mediator.Send(query, token);

            return NoContent();
        }
    }
}