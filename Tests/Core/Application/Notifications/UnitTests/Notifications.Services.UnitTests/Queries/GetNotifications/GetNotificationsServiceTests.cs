using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Notifications.Queries;
using Solen.Core.Application.Notifications.Services.Queries;

namespace Notifications.Services.UnitTests.Queries.GetNotifications
{
    [TestFixture]
    public class GetNotificationsServiceTests
    {
        private Mock<IGetNotificationsRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetNotificationsService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetNotificationsRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetNotificationsService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("userId");
        }
        
        [Test]
        public void GetNotifications_WhenCalled_ReturnUserNotificationsList()
        {
            var notifications = new List<NotificationDto>();
            _repo.Setup(x => x.GetNotifications("userId", default))
                .ReturnsAsync(notifications);

            var result = _sut.GetNotifications(default).Result;

            Assert.That(result, Is.EqualTo(notifications));
        }
    }
}