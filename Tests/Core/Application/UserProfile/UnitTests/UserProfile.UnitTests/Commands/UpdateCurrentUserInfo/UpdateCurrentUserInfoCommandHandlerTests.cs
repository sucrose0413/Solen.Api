using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.UserProfile.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UserProfile.UnitTests.Commands.UpdateCurrentUserInfo
{
    [TestFixture]
    public class UpdateCurrentUserInfoCommandHandlerTests
    {
        private Mock<IUpdateCurrentUserInfoService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateCurrentUserInfoCommand _command;
        private UpdateCurrentUserInfoCommandHandler _sut;

        private User _currentUser;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateCurrentUserInfoService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateCurrentUserInfoCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateCurrentUserInfoCommand {UserName = "user name",};

            _currentUser = new User("email", "organizationId");
            _service.Setup(x => x.GetCurrentUser(default))
                .ReturnsAsync(_currentUser);
        }

        [Test]
        public void WhenCalled_UpdateCurrentUserName()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCurrentUserName(_currentUser, _command.UserName));
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
                _service.Setup(x => x.UpdateCurrentUserName(_currentUser, _command.UserName)).InSequence();
                _service.Setup(x => x.UpdateCurrentUserRepo(_currentUser)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}