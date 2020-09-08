using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.UnitTests.Commands.UpdateLearningPath
{
    [TestFixture]
    public class UpdateLearningPathCommandHandlerTests
    {
        private UpdateLearningPathCommandHandler _sut;
        private Mock<IUpdateLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateLearningPathCommand _command;

        private LearningPath _learningPathToUpdate;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateLearningPathCommand
            {
                LearningPathId = "learningPathId",
                Name = "name",
                Description = "description"
            };

            _learningPathToUpdate = new LearningPath("name", "organizationId");
            _service.Setup(x => x.GetLearningPath(_command.LearningPathId, default))
                .ReturnsAsync(_learningPathToUpdate);
        }

        [Test]
        public void WhenCalled_UpdateLearningPathName()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateName(_learningPathToUpdate, _command.Name));
        }

        [Test]
        public void WhenCalled_UpdateLearningPathDescription()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateDescription(_learningPathToUpdate, _command.Description));
        }

        [Test]
        public void WhenCalled_UpdateLearningPathRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLearningPathRepo(_learningPathToUpdate));
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
                _service.Setup(x => x.UpdateName(_learningPathToUpdate, _command.Name))
                    .InSequence();
                _service.Setup(x => x.UpdateDescription(_learningPathToUpdate, _command.Description))
                    .InSequence();
                _service.Setup(x => x.UpdateLearningPathRepo(_learningPathToUpdate))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}