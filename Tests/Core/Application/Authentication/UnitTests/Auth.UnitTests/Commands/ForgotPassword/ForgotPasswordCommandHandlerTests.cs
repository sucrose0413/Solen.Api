using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.UnitTests.Commands.ForgotPassword
{
    [TestFixture]
    public class ForgotPasswordCommandHandlerTests
    {
        private ForgotPasswordCommandHandler _sut;
        private Mock<IForgotPasswordService> _service;
        private Mock<IUnitOfWork> _unitOfWork;

        private ForgotPasswordCommand _command;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IForgotPasswordService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new ForgotPasswordCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new ForgotPasswordCommand {Email = "email"};

            _user = new User("email", "organizationId");
            _service.Setup(x => x.GetUserFromRepo(_command.Email, default))
                .ReturnsAsync(_user);
        }

        [Test]
        public void WhenCalled_SetUserPasswordToken()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.SetUserPasswordToken(_user));
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
        public void WhenCalled_PublishPasswordTokenSetEvent()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.PublishPasswordTokenSetEvent(_user, default));
        }
        
        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.SetUserPasswordToken(_user)).InSequence();
                _service.Setup(x => x.UpdateUserRepo(_user)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _service.Setup(x => x.PublishPasswordTokenSetEvent(_user, default)).InSequence();
                
                _sut.Handle(_command, default).Wait();
            }
        }
    }
}