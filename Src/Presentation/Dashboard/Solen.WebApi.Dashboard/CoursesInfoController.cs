using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Dashboard.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Dashboard
{
    [Authorize(Policy = AuthorizationPolicies.InstructorPolicy)]
    [Route("api/dashboard/courses")]
    [SwaggerTag("(Dashboard)")]
    public class CoursesInfoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CoursesInfoViewModel>> GetInfo(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCoursesInfoQuery(), token));
        }
    }
}