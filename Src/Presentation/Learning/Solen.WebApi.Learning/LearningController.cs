using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.Learning.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Learning
{
    [Authorize(Policy = AuthorizationPolicies.LearnerPolicy)]
    [Route("api/learning/courses")]
    [SwaggerTag("(Learning)")]
    public class LearningController : BaseController
    {
        [HttpGet("list")]
        public async Task<ActionResult<LearnerCoursesListViewModel>> GetAll([FromQuery] GetCoursesListQuery query,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(query, token));
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<LearnerCourseContentViewModel>> GetCourse(string courseId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCourseContentQuery {CourseId = courseId}, token));
        }

        [HttpGet("{courseId}/overview")]
        public async Task<ActionResult<LearnerCourseOverviewViewModel>> GetCourseOverview(string courseId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCourseOverviewQuery {CourseId = courseId}, token));
        }

        [HttpPost("addLearnerAccessHistory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddLearnerAccessHistory(AddLearnerAccessHistoryCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpGet("{courseId}/completedLectures")]
        public async Task<ActionResult<CompletedLecturesViewModel>> GetCompletedLectures(string courseId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCompletedLecturesQuery(courseId), token));
        }

        [HttpPost("completeLecture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CompleteLecture(CompleteLectureCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpDelete("uncompleteLecture/{lectureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UncompleteLecture(string lectureId, CancellationToken token)
        {
            await Mediator.Send(new UncompleteLectureCommand(lectureId), token);

            return NoContent();
        }

        [HttpGet("filters")]
        public async Task<ActionResult<LearnerCoursesFiltersViewModel>> GetCoursesFilters(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCoursesFiltersQuery(), token));
        }
    }
}