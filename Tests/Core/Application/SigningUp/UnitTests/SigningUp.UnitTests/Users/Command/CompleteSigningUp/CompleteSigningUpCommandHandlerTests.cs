using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Identity.Entities;


namespace SigningUp.UnitTests.Users.Command.CompleteSigningUp
{
    [TestFixture]
    public class CompleteSigningUpCommandHandlerTests
    {
        private Mock<ICompleteUserSigningUpService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private CompleteUserSigningUpCommand _command;
        private CompleteSigningUpCommandHandler _sut;

        private User _user;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICompleteUserSigningUpService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _sut = new CompleteSigningUpCommandHandler(_service.Object, _unitOfWork.Object, _mediator.Object);

            _command = new CompleteUserSigningUpCommand
            {
                SigningUpToken = "Token",
                UserName = "user name",
                Password = "password"
            };

            _user = new User("email", "organization");
            _service.Setup(x => x.GetUserByInvitationToken(_command.SigningUpToken))
                .ReturnsAsync(_user);
        }

        [Test]
        public void WhenCalled_UpdateUserName()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserName(_user, _command.UserName));
        }

        [Test]
        public void WhenCalled_ValidateUserInscription()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ValidateUserInscription(_user, _command.Password));
        }

        [Test]
        public void WhenCalled_UpdateUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserRepo(_user));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void ChangesSaved_PublishSigningUpCompletedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<UserSigningUpCompletedEventNotification>(), default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.UpdateUserName(_user, _command.UserName)).InSequence();
                _service.Setup(x => x.ValidateUserInscription(_user, _command.Password)).InSequence();
                _service.Setup(x => x.UpdateUserRepo(_user)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<UserSigningUpCompletedEventNotification>(),
                    default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}