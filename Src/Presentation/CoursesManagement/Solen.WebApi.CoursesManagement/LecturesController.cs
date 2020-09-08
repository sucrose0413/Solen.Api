using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.CoursesManagement.Lectures.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/lectures")]
    [SwaggerTag("(Courses Management)")]
    public class LecturesController : BaseController
    {
        [HttpGet("{lectureId}")]
        public async Task<ActionResult<LectureViewModel>> GetLecture(string lectureId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLectureQuery {LectureId = lectureId}, token));
        }
    }
}