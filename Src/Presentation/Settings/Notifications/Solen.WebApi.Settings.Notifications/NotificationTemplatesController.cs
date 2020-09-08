using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Settings.Notifications.Commands;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Settings.Notifications
{
    [Authorize(Policy = AuthorizationPolicies.SettingsPolicy)]
    [Route("api/settings/notifications/templates")]
    [SwaggerTag("(Notifications Template Settings)")]
    public class NotificationTemplatesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<NotificationTemplatesViewModel>> GetTemplates(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetNotificationTemplatesQuery(), token));
        }

        [HttpGet("{templateId}")]
        public async Task<ActionResult<NotificationTemplateViewModel>> GetTemplate(string templateId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetNotificationTemplateQuery(templateId), token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTemplate(ToggleNotificationActivationCommand command,
            CancellationToken token)
        {
            await Mediator.Send(command, token);

            return NoContent();
        }
    }
}