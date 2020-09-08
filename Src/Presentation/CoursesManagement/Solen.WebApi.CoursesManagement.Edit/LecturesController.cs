using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement.Edit
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/lectures")]
    [SwaggerTag("(Courses Management)")]
    public class LecturesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> CreateLecture(CreateLectureCommand command,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateLecture(UpdateLectureCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpDelete("{lectureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CommandResponse>> DeleteLecture(string lectureId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new DeleteLectureCommand {LectureId = lectureId}, token));
        }


        [HttpPut("media/{lectureId}"), DisableRequestSizeLimit]
        public async Task<ActionResult> Upload(string lectureId, IFormFile file, [FromForm] string lectureType)
        {
            await Mediator.Send(new UploadMediaCommand(lectureId, lectureType, new ResourceFile(file)));

            return NoContent();
        }
    }
}