using System;
using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.LearningPaths.Commands.UpdateCourseLearningPaths
{
    [TestFixture]
    public class UpdateCourseLearningPathsCommandHandlerTests
    {
        private Mock<IUpdateCourseLearningPathsService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateCourseLearningPathsCommandHandler _sut;
        private UpdateCourseLearningPathsCommand _command;

        private Course _courseToUpdate;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateCourseLearningPathsService>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _sut = new UpdateCourseLearningPathsCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateCourseLearningPathsCommand
                {CourseId = "courseId", LearningPathsIds = new List<string>()};

            _courseToUpdate = new Course("course title", "creatorId", DateTime.Now);
            _service.Setup(x => x.GetCourseFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToUpdate);
        }

        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckCourseStatusForModification(_courseToUpdate));
        }

        [Test]
        public void DataControlIsOk_RemoveEventualLearningPaths()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x =>
                x.RemoveEventualLearningPaths(_courseToUpdate, _command.LearningPathsIds));
        }

        [Test]
        public void LearningPathsIdsListIsNotEmpty_AddTheNewLearningPaths()
        {
            var learningPathsIdsToAdd = new List<string> {"learningPathId1", "learningPathId2"};
            _service.Setup(x => x.GetLearningPathsIdsToAdd(_courseToUpdate, _command.LearningPathsIds))
                .Returns(learningPathsIdsToAdd);

            _sut.Handle(_command, default);

            _service.Verify(x =>
                x.AddLearningPathToCourse(_courseToUpdate, It.Is<string>(l => learningPathsIdsToAdd.Contains(l)),
                    default), Times.Exactly(learningPathsIdsToAdd.Count));
        }


        [Test]
        public void DataControlIsOk_UpdateCourseRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCourseRepo(_courseToUpdate), Times.Once);
        }

        [Test]
        public void DataControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }


        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckCourseStatusForModification(_courseToUpdate)).InSequence();
                _service
                    .Setup(x => x.RemoveEventualLearningPaths(_courseToUpdate, _command.LearningPathsIds)).InSequence();
                _service.Setup(x =>
                    x.GetLearningPathsIdsToAdd(_courseToUpdate, _command.LearningPathsIds)).InSequence();
                _service.Setup(x => x.UpdateCourseRepo(_courseToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}