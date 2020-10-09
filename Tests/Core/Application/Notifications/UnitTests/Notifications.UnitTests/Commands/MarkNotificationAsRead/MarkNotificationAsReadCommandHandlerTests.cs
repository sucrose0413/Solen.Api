using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Notifications.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Notifications.UnitTests.Commands.MarkNotificationAsRead
{
    [TestFixture]
    public class MarkNotificationAsReadCommandHandlerTests
    {
        private MarkNotificationAsReadCommandHandler _sut;
        private Mock<IMarkNotificationAsReadService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private MarkNotificationAsReadCommand _command;

        private Notification _notification;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IMarkNotificationAsReadService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new MarkNotificationAsReadCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new MarkNotificationAsReadCommand {NotificationId = "notificationId"};

            _notification = new Notification(CoursePublishedEvent.Instance, "recipient", "subject", "body");
            _service.Setup(x => x.GetNotification("notificationId", default))
                .ReturnsAsync(_notification);
        }

        [Test]
        public void NotificationIsNull_DoNoting()
        {
            _service.Setup(x => x.GetNotification("notificationId", default))
                .ReturnsAsync((Notification) null);

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateNotificationRepo(_notification), Times.Never);
        }

        [Test]
        public void NotificationIsNotNull_MarkTheNotificationAsRead()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.MarkNotificationAsRead(_notification));
        }

        [Test]
        public void NotificationIsNotNull_UpdateTheNotificationRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateNotificationRepo(_notification));
        }

        [Test]
        public void NotificationIsNotNull_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }
        
        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.MarkNotificationAsRead(_notification)).InSequence();
                _service.Setup(x => x.UpdateNotificationRepo(_notification)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}