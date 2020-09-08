using NUnit.Framework;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Domain.UnitTests.Notifications.Entities
{
    [TestFixture]
    public class NotificationTests
    {
        private Notification _sut;

        [Test]
        public void ConstructorWithNotificationEventRecipientIdSubjectBody_WhenCalled_SetPropertiesCorrectly()
        {
            var @event = new CoursePublishedEvent();
            
            _sut = new Notification(@event, "recipientId", "subject", "body");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.NotificationEvent, Is.EqualTo(@event));
            Assert.That(_sut.RecipientId, Is.EqualTo("recipientId"));
            Assert.That(_sut.Subject, Is.EqualTo("subject"));
            Assert.That(_sut.Body, Is.EqualTo("body"));
            Assert.That(_sut.CreationDate, Is.Not.Null);
            Assert.That(_sut.IsRead, Is.False);
        }

        [Test]
        public void MarkAsRead_WhenCalled_MarkNotificationAsRead()
        {
            _sut = new Notification(new CoursePublishedEvent(), "recipientId", "subject", "body");

            _sut.MarkAsRead();

            Assert.That(_sut.IsRead, Is.True);
        }
    }
}