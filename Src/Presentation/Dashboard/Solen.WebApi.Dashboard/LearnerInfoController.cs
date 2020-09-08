using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Dashboard.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Dashboard
{
    [Authorize(Policy = AuthorizationPolicies.LearnerPolicy)]
    [Route("api/dashboard/learner")]
    [SwaggerTag("(Dashboard)")]
    public class LearnerInfoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<LearnerInfoViewModel>> GetLearnerInfo(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetLearnerInfoQuery(), token));
        }
    }
}