using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace LearningPaths.UnitTests.Commands.DeleteLearningPath
{
    [TestFixture]
    public class DeleteLearningPathCommandHandlerTests
    {
        private DeleteLearningPathCommandHandler _sut;
        private Mock<IDeleteLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private DeleteLearningPathCommand _command;

        private LearningPath _learningPathToDelete;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IDeleteLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new DeleteLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new DeleteLearningPathCommand {LearningPathId = "learningPathId"};

            _learningPathToDelete = new LearningPath("name", "organizationId");
            _service.Setup(x => x.GetLearningPath(_command.LearningPathId, default))
                .ReturnsAsync(_learningPathToDelete);
        }

        [Test]
        public void WhenCalled_CheckIfTheLearningPathIsDeletable()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckIfDeletable(_learningPathToDelete));
        }

        [Test]
        public void LearningPathIsDeletableAndItHasUsers_ChangeLearningPathCurrentUsersToGeneral()
        {
            // Arrange
            var learningPathUsers = new List<User> {new User("email", "organizationId")};
            _service.Setup(x => x.GetLearningPathUsers(_learningPathToDelete.Id, default))
                .ReturnsAsync(learningPathUsers);
            var generalLearningPath = new LearningPath("General", "organizationId", isDeletable: false);
            _service.Setup(x => x.GetGeneralLearningPath(default))
                .ReturnsAsync(generalLearningPath);

            // Act
            _sut.Handle(_command, default).Wait();

            //Assert
            _service.Verify(x => x.ChangeUsersLearningPathToGeneral(learningPathUsers, generalLearningPath));
            _service.Verify(x => x.UpdateUsersRepo(learningPathUsers));
        }

        [Test]
        public void LearningPathIsDeletable_RemoveTheLearningPathFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveLearningPathFromRepo(_learningPathToDelete));
        }

        [Test]
        public void LearningPathIsDeletable_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }
        
        [Test]
        public void ChangesSaved_ReturnDeletedLearningPathId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_learningPathToDelete.Id));
        }
        
        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckIfDeletable(_learningPathToDelete)).InSequence();
                _service.Setup(x => x.RemoveLearningPathFromRepo(_learningPathToDelete)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}