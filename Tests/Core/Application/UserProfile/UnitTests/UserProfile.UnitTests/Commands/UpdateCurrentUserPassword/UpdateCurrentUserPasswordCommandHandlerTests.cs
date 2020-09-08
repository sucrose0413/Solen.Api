using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.UserProfile.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UserProfile.UnitTests.Commands.UpdateCurrentUserPassword
{
    [TestFixture]
    public class UpdateCurrentUserPasswordCommandHandlerTests
    {
        private Mock<IUpdateCurrentUserPasswordService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateCurrentUserPasswordCommand _command;
        private UpdateCurrentUserPasswordCommandHandler _sut;

        private User _currentUser;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateCurrentUserPasswordService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateCurrentUserPasswordCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateCurrentUserPasswordCommand {NewPassword = "ne password",};

            _currentUser = new User("email", "organizationId");
            _service.Setup(x => x.GetCurrentUser(default))
                .ReturnsAsync(_currentUser);
        }

        [Test]
        public void WhenCalled_UpdateCurrentUserPassword()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCurrentUserPassword(_currentUser, _command.NewPassword));
        }

        [Test]
        public void WhenCalled_UpdateCurrentUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCurrentUserRepo(_currentUser));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.UpdateCurrentUserPassword(_currentUser, _command.NewPassword)).InSequence();
                _service.Setup(x => x.UpdateCurrentUserRepo(_currentUser)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}