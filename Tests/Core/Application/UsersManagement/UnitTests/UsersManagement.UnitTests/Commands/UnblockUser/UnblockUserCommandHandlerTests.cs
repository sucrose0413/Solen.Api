using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.UnitTests.Commands.UnblockUser
{
    [TestFixture]
    public class UnblockUserCommandHandlerTests
    {
        private Mock<IUnblockUserService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UnblockUserCommand _command;
        private UnblockUserCommandHandler _sut;

        private User _userToUnblock;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUnblockUserService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UnblockUserCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UnblockUserCommand {UserId = "userId",};

            _userToUnblock = new User("email", "organizationId");
            _service.Setup(x => x.GetUser(_command.UserId, default))
                .ReturnsAsync(_userToUnblock);
        }


        [Test]
        public void WhenCalled_UnblockTheUser()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UnblockUser(_userToUnblock));
        }

        [Test]
        public void WhenCalled_UpdateTheUnblockedUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUser(_userToUnblock));
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
                _service.Setup(x => x.UnblockUser(_userToUnblock)).InSequence();
                _service.Setup(x => x.UpdateUser(_userToUnblock)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}