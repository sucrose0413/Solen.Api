using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.UnitTests.Commands.BlockUser
{
    [TestFixture]
    public class BlockUserCommandHandlerTests
    {
        private Mock<IBlockUserService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private BlockUserCommand _command;
        private BlockUserCommandHandler _sut;

        private User _userToBlock;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IBlockUserService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new BlockUserCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new BlockUserCommand {UserId = "userId",};

            _userToBlock = new User("email", "organizationId");
            _service.Setup(x => x.GetUser(_command.UserId, default))
                .ReturnsAsync(_userToBlock);
        }

        [Test]
        public void WhenCalled_CheckIfTheUserToBlockIsTheCurrentUser()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckIfTheUserToBlockIsTheCurrentUser(_command.UserId));
        }

        [Test]
        public void WhenCalled_BlockTheUser()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.BlockUser(_userToBlock));
        }
        
        [Test]
        public void WhenCalled_UpdateTheBlockedUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUser(_userToBlock));
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
                _service.Setup(x => x.CheckIfTheUserToBlockIsTheCurrentUser(_command.UserId)).InSequence();
                _service.Setup(x => x.BlockUser(_userToBlock)).InSequence();
                _service.Setup(x => x.UpdateUser(_userToBlock)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}