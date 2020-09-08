using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.SigningUp.EventsHandlers.Users;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace SigningUp.EventsHandlers.UnitTests.Users.SigningUpCompleted
{
    [TestFixture]
    public class SendNotificationToTheUserTests
    {
        private SendNotificationToTheUser _sut;
        private Mock<INotificationMessageHandler> _notificationHandler;

        private UserSigningUpCompletedEventNotification _event;

        [SetUp]
        public void SetUp()
        {
            _notificationHandler = new Mock<INotificationMessageHandler>();

            _sut = new SendNotificationToTheUser(_notificationHandler.Object);

            var user = new User("email", "organizationId");
            _event = new UserSigningUpCompletedEventNotification(user);
        }

        [Test]
        public void WhenCalled_TheRecipientShouldHaveTheCorrectEmail()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.Is<RecipientContactInfo>(r => r.Email == _event.User.Email),
                    It.IsAny<NotificationEvent>(), It.IsAny<NotificationData>()));
        }
        
        [Test]
        public void WhenCalled_SendTheCorrectEventType()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<UserSigningUpCompletedEvent>(),
                    It.IsAny<NotificationData>()));
        }
    }
}