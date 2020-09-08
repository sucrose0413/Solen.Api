using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.UnitTests.Commands.ResetPassword
{
    [TestFixture]
    public class ResetPasswordCommandHandlerTests
    {
        private ResetPasswordCommandHandler _sut;
        private Mock<IResetPasswordService> _service;
        private Mock<IUnitOfWork> _unitOfWork;

        private ResetPasswordCommand _command;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IResetPasswordService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new ResetPasswordCommandHandler(_service.Object, _unitOfWork.Object);
            _command = new ResetPasswordCommand {PasswordToken = "passwordToken", NewPassword = "password"};

            _user = new User("email", "organizationId");
            _service.Setup(x => x.GetUserByPasswordToken(_command.PasswordToken, default))
                .ReturnsAsync(_user);
        }

        [Test]
        public void WhenCalled_UpdateUserPassword()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserPassword(_user, _command.NewPassword));
        }

        [Test]
        public void WhenCalled_InitUserPasswordToken()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.InitUserPasswordToken(_user));
        }

        [Test]
        public void WhenCalled_UpdateUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserRepo(_user));
        }

        [Test]
        public void WhenCalled_SaveTheChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.UpdateUserPassword(_user, _command.NewPassword)).InSequence();
                _service.Setup(x => x.InitUserPasswordToken(_user)).InSequence();
                _service.Setup(x => x.UpdateUserRepo(_user)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}