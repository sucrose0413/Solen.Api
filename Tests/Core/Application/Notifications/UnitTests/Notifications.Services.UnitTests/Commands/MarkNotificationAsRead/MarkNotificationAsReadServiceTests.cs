using Moq;
using NUnit.Framework;
using Solen.Core.Application.Notifications.Services.Commands;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Notifications.Services.UnitTests.Commands.MarkNotificationAsRead
{
    [TestFixture]
    public class MarkNotificationAsReadServiceTests
    {
        private Mock<IMarkNotificationAsReadRepository> _repo;
        private MarkNotificationAsReadService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IMarkNotificationAsReadRepository>();
            _sut = new MarkNotificationAsReadService(_repo.Object);
        }

        [Test]
        public void GetNotification_WhenCalled_ReturnTheNotification()
        {
            var notification = new Notification(new CoursePublishedEvent(), "recipient", "subject", "body");
            _repo.Setup(x => x.GetNotificationById("notificationId", default))
                .ReturnsAsync(notification);

            var result = _sut.GetNotification("notificationId", default).Result;

            Assert.That(result, Is.EqualTo(notification));
        }

        [Test]
        public void MarkNotificationAsRead_WhenCalled_MarkTheNotificationAsRead()
        {
            var notification =
                new Mock<Notification>(new CoursePublishedEvent(), "recipient", "subject", "body");

            _sut.MarkNotificationAsRead(notification.Object);

            notification.Verify(x => x.MarkAsRead());
        }
        
        [Test]
        public void UpdateNotificationRepo_WhenCalled_UpdateNotificationRepo()
        {
            var notification = new Notification(new CoursePublishedEvent(), "recipient", "subject", "body");

             _sut.UpdateNotificationRepo(notification);

            _repo.Verify(x => x.UpdateNotification(notification));
        }
    }
}