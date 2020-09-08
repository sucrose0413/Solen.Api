using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Users.EventsHandlers.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace UsersManagement.EventsHandlers.UnitTests.Commands.InviteMembers
{
    [TestFixture]
    public class SendNotificationToUsersToCompleteSigningUpTests
    {
        private SendNotificationToUsersToCompleteSigningUp _sut;
        private Mock<INotificationMessageHandler> _notificationHandler;
        private Mock<IOptions<CompleteUserSigningUpPageInfo>> _signingUpPageOptions;

        private Solen.Core.Application.Users.Commands.MembersInvitedEvent _event;
        private CompleteUserSigningUpPageInfo _completeSigningUpPage;

        [SetUp]
        public void SetUp()
        {
            _notificationHandler = new Mock<INotificationMessageHandler>();
            _signingUpPageOptions = new Mock<IOptions<CompleteUserSigningUpPageInfo>>();

            _sut = new SendNotificationToUsersToCompleteSigningUp(_notificationHandler.Object,
                _signingUpPageOptions.Object);

            var users = new List<User> {new User("email", "organizationId")};
            _event = new Solen.Core.Application.Users.Commands.MembersInvitedEvent(users);

            _completeSigningUpPage = new CompleteUserSigningUpPageInfo
                {Url = "completeSigningUpPageUrl", TokenParameterName = "tokenParameter"};
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(_completeSigningUpPage);
        }

        [Test]
        public void CompleteSigningUpPageUrlIsNull_ThrowArgumentNullException()
        {
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(new CompleteUserSigningUpPageInfo {Url = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TokenNameParameterIsNull_ThrowArgumentNullException()
        {
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(new CompleteUserSigningUpPageInfo {TokenParameterName = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void WhenCalled_TheRecipientShouldHaveTheCorrectEmail()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.Is<RecipientContactInfo>(r => _event.Users.Select(u => u.Email).Contains(r.Email)),
                    It.IsAny<NotificationEvent>(), It.IsAny<NotificationData>()), Times.Exactly(_event.Users.Count()));
        }

        [Test]
        public void WhenCalled_SendTheCorrectEventType()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<UserSigningUpInitializedEvent>(),
                    It.IsAny<NotificationData>()), Times.Exactly(_event.Users.Count()));
        }

        [Test]
        public void WhenCalled_TheSigningUpPageLinkShouldContainTheCorrectUrl()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<NotificationEvent>(),
                    It.Is<SigningUpInfo>(d => d.LinkToCompleteSigningUp.Contains(_completeSigningUpPage.Url))));
        }

        [Test]
        public void WhenCalled_TheSigningUpPageLinkShouldContainTheCorrectTokenParameterName()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<NotificationEvent>(),
                    It.Is<SigningUpInfo>(d =>
                        d.LinkToCompleteSigningUp.Contains(_completeSigningUpPage.TokenParameterName))));
        }
    }
}