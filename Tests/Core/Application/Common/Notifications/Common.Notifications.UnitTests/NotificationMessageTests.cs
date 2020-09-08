using NUnit.Framework;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Common.Notifications.UnitTests
{
    [TestFixture]
    public class NotificationMessageTests
    {
        private NotificationMessage _sut;

        [Test]
        public void ConstructorWithSubjectBodyNotificationEvent_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            var @event = new CoursePublishedEvent();
                
            _sut = new NotificationMessage("subject", "body", @event);

            Assert.That(_sut.Subject, Is.EqualTo("subject"));
            Assert.That(_sut.Body, Is.EqualTo("body"));
            Assert.That(_sut.NotificationEvent, Is.EqualTo(@event));
        }
    }
}