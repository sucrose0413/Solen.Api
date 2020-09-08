using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.UnitTests.Commands.UpdateCoursesOrders
{
    [TestFixture]
    public class UpdateCoursesOrdersCommandHandlerTests
    {
        private UpdateCoursesOrdersCommandHandler _sut;
        private Mock<IUpdateCoursesOrdersService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateCoursesOrdersCommand _command;

        private List<LearningPathCourse> _coursesToUpdateOrders;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateCoursesOrdersService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateCoursesOrdersCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateCoursesOrdersCommand
                {LearningPathId = "learningPathId", CoursesOrders = new List<CourseOrderDto>()};

            _coursesToUpdateOrders = new List<LearningPathCourse>();
            _service.Setup(x => x.GetLearningPathCourses(_command.LearningPathId, default))
                .ReturnsAsync(_coursesToUpdateOrders);
        }
        
        [Test]
        public void WhenCalled_UpdateCoursesOrders()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCoursesOrders(_coursesToUpdateOrders, _command.CoursesOrders));
        }
        
        [Test]
        public void WhenCalled_UpdateLearningPathCoursesRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLearningPathCoursesRepo(_coursesToUpdateOrders));
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
                _service.Setup(x => x.UpdateCoursesOrders(_coursesToUpdateOrders, _command.CoursesOrders))
                    .InSequence();
                _service.Setup(x => x.UpdateLearningPathCoursesRepo(_coursesToUpdateOrders))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}