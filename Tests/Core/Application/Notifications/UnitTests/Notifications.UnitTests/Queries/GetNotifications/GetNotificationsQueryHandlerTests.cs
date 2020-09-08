using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Notifications.Queries;

namespace Notifications.UnitTests.Queries.GetNotifications
{
    [TestFixture]
    public class GetNotificationsQueryHandlerTests
    {
        private GetNotificationsQueryHandler _sut;
        private Mock<IGetNotificationsService> _service;
        private GetNotificationsQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetNotificationsService>();
            _sut = new GetNotificationsQueryHandler(_service.Object);

            _query = new GetNotificationsQuery();
        }

        [Test]
        public void WhenCalled_ReturnNotificationList()
        {
            var notifications = new List<NotificationDto>();
            _service.Setup(x => x.GetNotifications(default))
                .ReturnsAsync(notifications);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Notifications, Is.EqualTo(notifications));
        }
    }
}