using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.LearningPaths
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/learning-paths")]
    [SwaggerTag("(Learning Paths)")]
    public class LearningPathsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<LearningPathsViewModel>> GetAll(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearningPathsQuery(), token));
        }

        [HttpGet("{learningPathId}")]
        public async Task<ActionResult<LearningPathViewModel>> GetById(string learningPathId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearningPathQuery(learningPathId), token));
        }

        [HttpGet("{learningPathId}/courses")]
        public async Task<ActionResult<LearningPathCoursesViewModel>> GetLearningPathCourses(string learningPathId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearningPathCoursesQuery(learningPathId), token));
        }

        [HttpGet("{learningPathId}/courses-to-add")]
        public async Task<ActionResult<OtherCoursesToAddViewModel>> GetOtherCoursesToAdd(string learningPathId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetOtherCoursesToAddQuery(learningPathId), token));
        }
        
        [HttpPut("update-courses-orders")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCoursesOrders(UpdateCoursesOrdersCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpGet("{learningPathId}/learners")]
        public async Task<ActionResult<LearningPathLearnersViewModel>> GetLearningPathLearners(string learningPathId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearningPathLearnersQuery(learningPathId), token));
        }

        [HttpGet("learner-progress/{learnerId}")]
        public async Task<ActionResult<LearnerProgressViewModel>> GetLearnerProgress(string learnerId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearnerProgressQuery(learnerId), token));
        }

        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Create(CreateLearningPathCommand command,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpDelete("{learningPathId}")]
        public async Task<ActionResult<CommandResponse>> Delete(string learningPathId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new DeleteLearningPathCommand {LearningPathId = learningPathId}, token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateLearningPath(UpdateLearningPathCommand command, CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPost("add-courses")]
        public async Task<ActionResult> AddCoursesToLearningPaths(AddCoursesToLearningPathCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpPut("remove-course")]
        public async Task<ActionResult> RemoveLearningPathCourse(RemoveCourseFromLearningPathCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}