using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement.Edit
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/courses")]
    [SwaggerTag("(Courses Management)")]
    public class CoursesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> CreateCourse(CreateCourseCommand command,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCourse(UpdateCourseCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public async Task<ActionResult<CommandResponse>> DeleteCourse(string courseId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new DeleteCourseCommand {CourseId = courseId}, token));
        }

        [HttpPut("modulesOrders")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateModulesOrders(UpdateModulesOrdersCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPut("publish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PublishCourse(PublishCourseCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPut("unpublish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UnpublishCourse(UnpublishCourseCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPut("draft")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DraftCourse(DraftCourseCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}