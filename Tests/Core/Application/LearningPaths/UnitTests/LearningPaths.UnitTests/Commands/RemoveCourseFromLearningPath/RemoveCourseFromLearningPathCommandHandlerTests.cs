using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.UnitTests.Commands.RemoveCourseFromLearningPath
{
    [TestFixture]
    public class RemoveCourseFromLearningPathCommandHandlerTests
    {
        private RemoveCourseFromLearningPathCommandHandler _sut;
        private Mock<IRemoveCourseFromLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private RemoveCourseFromLearningPathCommand _command;

        private LearningPathCourse _learningPathCourseToDelete;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IRemoveCourseFromLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new RemoveCourseFromLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new RemoveCourseFromLearningPathCommand
                {LearningPathId = "learningPathId", CourseId = "courseId"};

            _learningPathCourseToDelete = new LearningPathCourse("learningPathId", "courseId", 1);
            _service.Setup(x => x.GetLearningPathCourseFromRepo(_command.LearningPathId, _command.CourseId, default))
                .ReturnsAsync(_learningPathCourseToDelete);
        }

        [Test]
        public void LearningPathCourseIsNull_DoNothing()
        {
            _service.Setup(x => x.GetLearningPathCourseFromRepo(_command.LearningPathId, _command.CourseId, default))
                .ReturnsAsync((LearningPathCourse) null);

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveLearningPathCourseFromRepo(It.IsAny<LearningPathCourse>()),
                Times.Never);
        }

        [Test]
        public void LearningPathCourseIsNotNull_RemoveLearningPathCourseFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveLearningPathCourseFromRepo(_learningPathCourseToDelete));
        }

        [Test]
        public void LearningPathCourseIsNotNull_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.RemoveLearningPathCourseFromRepo(_learningPathCourseToDelete))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}