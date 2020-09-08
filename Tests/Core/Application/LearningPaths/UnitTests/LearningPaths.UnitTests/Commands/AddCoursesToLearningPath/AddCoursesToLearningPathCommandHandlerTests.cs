using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.UnitTests.Commands.AddCoursesToLearningPath
{
    [TestFixture]
    public class AddCoursesToLearningPathCommandHandlerTests
    {
        private AddCoursesToLearningPathCommandHandler _sut;
        private Mock<IAddCoursesToLearningPathService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private AddCoursesToLearningPathCommand _command;

        private LearningPath _learningPathToUpdate;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IAddCoursesToLearningPathService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new AddCoursesToLearningPathCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new AddCoursesToLearningPathCommand {CoursesIds = new List<string> {"courseId"}};

            _learningPathToUpdate = new LearningPath("name", "organizationId");
            _service.Setup(x => x.GetLearningPathFromRepo(_command.LearningPathId, default))
                .ReturnsAsync(_learningPathToUpdate);
        }

        [Test]
        public void WhenCalled_CheckExistenceForEachCourseId()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.DoesCourseExist(It.IsIn<string>(_command.CoursesIds), default),
                Times.Exactly(_command.CoursesIds.Count));
        }

        [Test]
        public void ExistenceControlIsOk_AddCourseToLearningPath()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x =>
                    x.AddCourseToLearningPath(_learningPathToUpdate, It.IsIn<string>(_command.CoursesIds)),
                Times.Exactly(_command.CoursesIds.Count));
        }

        [Test]
        public void ExistenceControlIsOk_UpdateLearningPathRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLearningPathRepo(_learningPathToUpdate));
        }

        [Test]
        public void ExistenceControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.DoesCourseExist(It.IsIn<string>(_command.CoursesIds), default))
                    .InSequence();
                _service.Setup(x =>
                        x.AddCourseToLearningPath(_learningPathToUpdate, It.IsIn<string>(_command.CoursesIds)))
                    .InSequence();
                _service.Setup(x => x.UpdateLearningPathRepo(_learningPathToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}