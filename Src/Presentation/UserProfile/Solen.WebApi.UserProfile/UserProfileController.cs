using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.UserProfile.Commands;
using Solen.Core.Application.UserProfile.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.UserProfile
{
    [Route("api/user-profile")]
    [SwaggerTag("(User Profile)")]
    public class UserProfileController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserProfileViewModel>> GetCurrentUserInfo(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetUserProfileQuery(), token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCurrentUserInfo(UpdateCurrentUserInfoCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
        
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePassword(UpdateCurrentUserPasswordCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}