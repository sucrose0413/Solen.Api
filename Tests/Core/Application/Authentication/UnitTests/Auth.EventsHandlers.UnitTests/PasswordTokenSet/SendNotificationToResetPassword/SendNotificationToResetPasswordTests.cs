using System;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Auth.EventsHandlers.PasswordTokenSet;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Auth.EventsHandlers.UnitTests
{
    [TestFixture]
    public class SendNotificationToResetPasswordTests
    {
        private SendNotificationToResetPassword _sut;
        private Mock<INotificationMessageHandler> _notificationHandler;
        private Mock<IOptions<ResetPasswordPageInfo>> _resetPageOptions;

        private PasswordTokenSetEvent _event;
        private ResetPasswordPageInfo _resetPage;

        [SetUp]
        public void SetUp()
        {
            _notificationHandler = new Mock<INotificationMessageHandler>();
            _resetPageOptions = new Mock<IOptions<ResetPasswordPageInfo>>();

            _sut = new SendNotificationToResetPassword(_notificationHandler.Object, _resetPageOptions.Object);

            var user = new User("email", "organizationId");
            _event = new PasswordTokenSetEvent(user);

            _resetPage = new ResetPasswordPageInfo {Url = "resetPageUrl", TokenParameterName = "tokenParameter"};
            _resetPageOptions.Setup(x => x.Value)
                .Returns(_resetPage);
        }

        [Test]
        public void ResetPageUrlIsNull_ThrowArgumentNullException()
        {
            _resetPageOptions.Setup(x => x.Value)
                .Returns(new ResetPasswordPageInfo {Url = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TokenNameParameterIsNull_ThrowArgumentNullException()
        {
            _resetPageOptions.Setup(x => x.Value)
                .Returns(new ResetPasswordPageInfo {TokenParameterName = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
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
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<PasswordForgottenEvent>(),
                    It.IsAny<NotificationData>()));
        }

        [Test]
        public void WhenCalled_TheResetPageLinkShouldContainTheCorrectUrl()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<NotificationEvent>(),
                    It.Is<ResetPasswordInfo>(d => d.LinkToResetPassword.Contains(_resetPage.Url))));
        }

        [Test]
        public void WhenCalled_TheResetPageLinkShouldContainTheCorrectTokenParameterName()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<NotificationEvent>(),
                    It.Is<ResetPasswordInfo>(d => d.LinkToResetPassword.Contains(_resetPage.TokenParameterName))));
        }
    }
}