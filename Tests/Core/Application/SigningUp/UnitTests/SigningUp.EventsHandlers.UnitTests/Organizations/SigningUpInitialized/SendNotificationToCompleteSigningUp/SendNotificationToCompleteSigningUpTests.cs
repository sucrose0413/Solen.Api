using System;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.SigningUp.EventsHandlers.Organizations;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Subscription.Entities;

namespace SigningUp.EventsHandlers.UnitTests.Organizations.SigningUpInitialized
{
    [TestFixture]
    public class SendNotificationToCompleteSigningUpTests
    {
        private SendNotificationToCompleteSigningUp _sut;
        private Mock<INotificationMessageHandler> _notificationHandler;
        private Mock<IOptions<CompleteOrganizationSigningUpPageInfo>> _signingUpPageOptions;

        private SigningUpInitializedEvent _event;
        private CompleteOrganizationSigningUpPageInfo _completeSigningUpPage;

        [SetUp]
        public void SetUp()
        {
            _notificationHandler = new Mock<INotificationMessageHandler>();
            _signingUpPageOptions = new Mock<IOptions<CompleteOrganizationSigningUpPageInfo>>();

            _sut = new SendNotificationToCompleteSigningUp(_notificationHandler.Object, _signingUpPageOptions.Object);

            var signingUp = new OrganizationSigningUp("email", "token");
            _event = new SigningUpInitializedEvent(signingUp);

            _completeSigningUpPage = new CompleteOrganizationSigningUpPageInfo
                {Url = "completeSigningUpPageUrl", TokenParameterName = "tokenParameter"};
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(_completeSigningUpPage);
        }

        [Test]
        public void CompleteSigningUpPageUrlIsNull_ThrowArgumentNullException()
        {
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(new CompleteOrganizationSigningUpPageInfo {Url = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TokenNameParameterIsNull_ThrowArgumentNullException()
        {
            _signingUpPageOptions.Setup(x => x.Value)
                .Returns(new CompleteOrganizationSigningUpPageInfo {TokenParameterName = null});

            Assert.That(() => _sut.Handle(_event, default),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void WhenCalled_TheRecipientShouldHaveTheCorrectEmail()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.Is<RecipientContactInfo>(r => r.Email == _event.SigningUp.Email),
                    It.IsAny<NotificationEvent>(), It.IsAny<NotificationData>()));
        }

        [Test]
        public void WhenCalled_SendTheCorrectEventType()
        {
            _sut.Handle(_event, default).Wait();

            _notificationHandler.Verify(x =>
                x.Handle(It.IsAny<RecipientContactInfo>(), It.IsAny<OrganizationSigningUpInitializedEvent>(),
                    It.IsAny<NotificationData>()));
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