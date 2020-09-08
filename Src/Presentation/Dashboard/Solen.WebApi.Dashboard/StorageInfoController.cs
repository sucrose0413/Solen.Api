using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Dashboard.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;


namespace Solen.WebApi.Dashboard
{
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    [Route("api/dashboard/storage")]
    [SwaggerTag("(Dashboard)")]
    public class StorageInfoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<StorageInfoViewModel>> GetInfo(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetStorageInfoQuery(), token));
        }
    }
}