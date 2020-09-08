using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Subscription.Entities;

namespace SigningUp.UnitTests.Organizations.Commands.InitSigningUp
{
    [TestFixture]
    public class InitSigningUpCommandHandlerTests
    {
        private Mock<IInitOrganizationSigningUpService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private InitSigningUpCommand _command;
        private InitSigningUpCommandHandler _sut;

        private OrganizationSigningUp _signingUp;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IInitOrganizationSigningUpService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _command = new InitSigningUpCommand {Email = "email@example.com"};
            _sut = new InitSigningUpCommandHandler(_service.Object, _unitOfWork.Object, _mediator.Object);

            _signingUp = new OrganizationSigningUp(_command.Email, "token");
            _service.Setup(x => x.InitOrganizationSigningUp(_command.Email)).Returns(_signingUp);
        }

        [Test]
        public void WhenCalled_CheckIfSigningUpIsEnabled()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckIfSigningUpIsEnabled());
        }

        [Test]
        public void SigningUpIsEnabled_CheckEmailExistence()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckEmailExistence(_command.Email));
        }

        [Test]
        public void ControlIsOk_AddTheSigningUpToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddOrganizationSigningUpToRepo(_signingUp, default));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void SigningUpInitialized_PublishSigningUpInitializedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<SigningUpInitializedEvent>(),
                default));
        }


        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckEmailExistence(_command.Email)).InSequence();
                _service.Setup(x => x.AddOrganizationSigningUpToRepo(_signingUp, default)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<SigningUpInitializedEvent>(),
                    default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}