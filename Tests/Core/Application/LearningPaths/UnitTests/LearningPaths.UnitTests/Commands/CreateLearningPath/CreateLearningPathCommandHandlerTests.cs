using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.UnitTests.Commands.CreateLearningPath
{
    [TestFixture]
    public class CreateLearningPathCommandHandlerTests
    {
        private CreateLearningPathCommandHandler _sut;
        private Mock<ICreateLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private CreateLearningPathCommand _command;

        private LearningPath _learningPathToCreate;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICreateLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new CreateLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new CreateLearningPathCommand {Name = "name"};

            _learningPathToCreate = new LearningPath(_command.Name, "organizationId");
            _service.Setup(x => x.CreateLearningPath(_command.Name))
                .Returns(_learningPathToCreate);
        }

        [Test]
        public void WhenCalled_AddLearningPathToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddLearningPathToRepo(_learningPathToCreate, default));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }
        
        [Test]
        public void ChangesSaved_ReturnCreatedLearningPathId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_learningPathToCreate.Id));
        }
        
        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.AddLearningPathToRepo(_learningPathToCreate, default))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}