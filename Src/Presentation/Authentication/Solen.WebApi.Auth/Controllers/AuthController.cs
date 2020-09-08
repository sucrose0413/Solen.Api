using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Auth.Queries;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Solen.Core.Application.Auth.Commands;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Auth.Controllers
{
    [Route("api/auth")]
    [SwaggerTag("(Authentication)")]
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserViewModel>> Login([FromBody] LoginUserQuery query,
            CancellationToken token)
        {
            return await Mediator.Send(query, token);
        }

        [HttpGet("currentUser")]
        public async Task<ActionResult<LoggedUserDto>> CurrentUser(CancellationToken token)
        {
            return await Mediator.Send(new GetCurrentLoggedUserQuery(), token);
        }

        [AllowAnonymous]
        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<ActionResult<LoggedUserViewModel>> RefreshToken(string refreshToken, CancellationToken token)
        {
            return await Mediator.Send(new RefreshTokenQuery(refreshToken), token);
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("checkPasswordToken")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CheckPasswordToken(CheckPasswordTokenQuery query, CancellationToken token)
        {
            await Mediator.Send(query, token);

            return NoContent();
        }
    }
}