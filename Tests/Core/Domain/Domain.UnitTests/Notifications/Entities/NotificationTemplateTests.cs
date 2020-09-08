using NUnit.Framework;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Domain.UnitTests.Notifications.Entities
{
    [TestFixture]
    public class NotificationTemplateTests
    {
        private NotificationTemplate _sut;


        [Test]
        public void ConstructorWithNotificationTypeNotificationEvent_WhenCalled_SetPropertiesCorrectly()
        {
            var @event = new CoursePublishedEvent();
            var notificationType = new EmailNotification();

            _sut = new NotificationTemplate(notificationType, @event, isSystemNotification: true);

            Assert.That(_sut.TypeName, Is.EqualTo(notificationType.Name));
            Assert.That(_sut.NotificationEventName, Is.EqualTo(@event.Name));
            Assert.That(_sut.IsSystemNotification, Is.True);
        }

        [Test]
        public void UpdateTemplateSubject_WhenCalled_UpdateTemplateSubject()
        {
            _sut = new NotificationTemplate(new EmailNotification(), new CoursePublishedEvent(),
                isSystemNotification: true);

            _sut.UpdateTemplateSubject("new subject");

            Assert.That(_sut.TemplateSubject, Is.EqualTo("new subject"));
        }

        [Test]
        public void UpdateTemplateBody_WhenCalled_UpdateTemplateBody()
        {
            _sut = new NotificationTemplate(new EmailNotification(), new CoursePublishedEvent(),
                isSystemNotification: true);

            _sut.UpdateTemplateBody("new body");

            Assert.That(_sut.TemplateBody, Is.EqualTo("new body"));
        }
    }
}