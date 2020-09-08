using Moq;
using NUnit.Framework;
using Solen.Core.Application.Settings.Notifications.Commands;
using Solen.Core.Application.UnitOfWork;

namespace Settings.Notifications.UnitTests.Commands.ToggleNotificationActivation
{
    [TestFixture]
    public class ToggleNotificationActivationCommandHandlerTests
    {
        private ToggleNotificationActivationCommandHandler _sut;
        private Mock<IToggleNotificationActivationService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private ToggleNotificationActivationCommand _command;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IToggleNotificationActivationService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new ToggleNotificationActivationCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new ToggleNotificationActivationCommand {TemplateId = "templateId"};
        }

        [Test]
        public void NotificationTemplateIsActivated_ActivateNotificationTemplate()
        {
            _command.IsActivated = true;

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ActivateNotificationTemplate(_command.TemplateId, default));
            _service.Verify(x => x.DeactivateNotificationTemplate(_command.TemplateId, default), Times.Never);
        }
        
        [Test]
        public void NotificationTemplateIsNotActivated_DeactivateNotificationTemplate()
        {
            _command.IsActivated = false;

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.DeactivateNotificationTemplate(_command.TemplateId, default));
            _service.Verify(x => x.ActivateNotificationTemplate(_command.TemplateId, default), Times.Never);
        }
        
        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }
    }
}