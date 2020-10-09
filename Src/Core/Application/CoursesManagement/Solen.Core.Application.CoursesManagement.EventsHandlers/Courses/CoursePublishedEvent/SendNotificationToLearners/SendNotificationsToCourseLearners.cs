using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;


namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Courses
{
    public class SendNotificationsToCourseLearners : INotificationHandler<CoursePublishedEventNotification>
    {
        private readonly INotificationMessageHandler _notificationHandler;
        private readonly ISendNotificationsToCourseLearnersRepo _repo;

        public SendNotificationsToCourseLearners(INotificationMessageHandler notificationHandler,
            ISendNotificationsToCourseLearnersRepo repo)
        {
            _notificationHandler = notificationHandler;
            _repo = repo;
        }

        public async Task Handle(CoursePublishedEventNotification @event, CancellationToken token)
        {
            var courseInfo = await _repo.GetCourseInfo(@event.CourseId, token);
            var learners = await _repo.GetCourseLearners(@event.CourseId, token);

            foreach (var learner in learners)
                await _notificationHandler.Handle(learner, CoursePublishedEvent.Instance, courseInfo);
        }
    }
}