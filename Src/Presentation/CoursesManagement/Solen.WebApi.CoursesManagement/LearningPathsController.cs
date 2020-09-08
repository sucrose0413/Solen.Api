using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.CoursesManagement.LearningPaths.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/learning-paths")]
    [SwaggerTag("(Courses Management)")]
    public class LearningPathsController : BaseController
    {
        [HttpGet("{courseId}")]
        public async Task<ActionResult<GetCourseLearningPathsListVm>> GetCourseLearningPaths(string courseId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCourseLearningPathsListQuery {CourseId = courseId}, token));
        }
    }
}