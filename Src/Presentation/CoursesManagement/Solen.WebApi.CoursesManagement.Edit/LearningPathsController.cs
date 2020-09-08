using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement.Edit
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/learning-paths")]
    [SwaggerTag("(Courses Management)")]
    public class LearningPathsController : BaseController
    {
        [HttpPut]
        public async Task<ActionResult> UpdateCourseLearningPaths(UpdateCourseLearningPathsCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}