using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace CoursesManagement.EventsHandlers.UnitTests.Courses
{
    [TestFixture]
    public class SendNotificationsToCourseLearnersTests
    {
        private SendNotificationsToCourseLearners _sut;
        private Mock<ISendNotificationsToCourseLearnersRepo> _repo;
        private Mock<INotificationMessageHandler> _notificationHandler;

        private CoursePublishedEventNotification _event;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ISendNotificationsToCourseLearnersRepo>();
            _notificationHandler = new Mock<INotificationMessageHandler>();

            _sut = new SendNotificationsToCourseLearners(_notificationHandler.Object, _repo.Object);

            _event = new CoursePublishedEventNotification("courseId");
        }

        [Test]
        public void WhenCalled_SendNotificationToCourseLearners()
        {
            // Arrange
            var courseInfo = new CourseInfo("course name", "course creator");
            _repo.Setup(x => x.GetCourseInfo("courseId", default))
                .ReturnsAsync(courseInfo);
            var learners = new List<RecipientContactInfo>
                {new RecipientContactInfo(null, null), new RecipientContactInfo(null, null)};
            _repo.Setup(x => x.GetCourseLearners("courseId", default))
                .ReturnsAsync(learners);

            // Act
            _sut.Handle(_event, default).Wait();

            // Assert
            _notificationHandler.Verify(x =>
                x.Handle(It.IsIn<RecipientContactInfo>(learners), It.IsAny<CoursePublishedEvent>(),
                    courseInfo), Times.Exactly(learners.Count));
        }
    }
}