using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.Notifications.Commands;
using Solen.Core.Application.Notifications.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.Notifications
{
    [Route("api/notifications")]
    [SwaggerTag("(Notifications)")]
    public class NotificationsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<NotificationsListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetNotificationsQuery()));
        }

        [HttpPut("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateNotification(string notificationId)
        {
            await Mediator.Send(new MarkNotificationAsReadCommand {NotificationId = notificationId});

            return NoContent();
        }
    }
}