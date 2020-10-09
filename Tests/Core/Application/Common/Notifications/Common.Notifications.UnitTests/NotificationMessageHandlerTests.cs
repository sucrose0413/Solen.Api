using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Common.Notifications.UnitTests
{
    [TestFixture]
    public class NotificationMessageHandlerTests
    {
        private NotificationMessageHandler _sut;
        private Mock<INotificationsRepo> _repo;
        private Mock<INotificationMessageGenerator> _messageGenerator;
        private Mock<INotificationMessageDispatcher> _messageDispatcher;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        private NotificationEvent _event;
        private NotificationData _notificationData = null;
        private RecipientContactInfo _recipient = null;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<INotificationsRepo>();
            _messageGenerator = new Mock<INotificationMessageGenerator>();
            _messageDispatcher = new Mock<INotificationMessageDispatcher>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new NotificationMessageHandler(_repo.Object, _messageGenerator.Object, _messageDispatcher.Object,
                _currentUserAccessor.Object);


            _event = CoursePublishedEvent.Instance;
            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void WhenCalled_SendTheMessageToMessageDispatcher()
        {
            // Arrange
            var excludedTemplatesId = new List<string>();
            _repo.Setup(x => x.GetDisabledNotifications("organizationId"))
                .Returns(excludedTemplatesId);
            var templates = new List<NotificationTemplate>
                {new NotificationTemplate(EmailNotification.Instance, _event, false)};
            _repo.Setup(x => x.GetNotificationTemplatesByNotificationEvent(_event, excludedTemplatesId))
                .Returns(templates);
            var messageToSend = new NotificationMessage("subject", "body", _event);
            _messageGenerator.Setup(x => x.Generate(It.IsIn<NotificationTemplate>(templates), _notificationData))
                .Returns(messageToSend);

            // Act
            _sut.Handle(_recipient, _event, _notificationData).Wait();

            // Assert
            _messageDispatcher.Verify(x => x.Dispatch(It.IsAny<EmailNotification>(), messageToSend, _recipient));
        }
    }
}