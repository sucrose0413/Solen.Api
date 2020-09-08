using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.UnitTests.Commands.UpdateUserLearningPath
{
    [TestFixture]
    public class UpdateUserLearningPathCommandHandlerTests
    {
        private Mock<IUpdateUserLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateUserLearningPathCommand _command;
        private UpdateUserLearningPathCommandHandler _sut;

        private User _userToUpdate;
        private LearningPath _learningPath;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateUserLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateUserLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateUserLearningPathCommand {UserId = "userId", LearningPathId = "learningPathId"};

            _userToUpdate = new User("email", "organizationId");
            _service.Setup(x => x.GetUserFromRepo(_command.UserId, default))
                .ReturnsAsync(_userToUpdate);
            
            _learningPath = new LearningPath("name", "organizationId");
            _service.Setup(x => x.GetLearningPath(_command.LearningPathId, default))
                .ReturnsAsync(_learningPath);
        }

        [Test]
        public void WhenCalled_UpdateTheUserLearningPath()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserLearningPath(_userToUpdate, _learningPath));
        }

        [Test]
        public void WhenCalled_UpdateTheUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserRepo(_userToUpdate));
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
                _service.Setup(x => x.UpdateUserLearningPath(_userToUpdate, _learningPath)).InSequence();
                _service.Setup(x => x.UpdateUserRepo(_userToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}