using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Application.Users.Queries;

namespace Solen.WebApi.Users
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/users")]
    [SwaggerTag("(Users Management)")]
    public class UsersController : BaseController
    {
        [HttpPost("invite")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> InviteMembers(InviteMembersCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<UsersListViewModel>> GetUsersList(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetUsersListQuery(), token));
        }
        
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserViewModel>> GetUser(string userId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetUserQuery(userId), token));
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUser(UpdateUserLearningPathCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
        
        [HttpPut("roles-and-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUserRoles(UpdateUserRolesCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
        
        [HttpPut("block")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> BlockUser(BlockUserCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
        
        [HttpPut("unblock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> BlockUser(UnblockUserCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}