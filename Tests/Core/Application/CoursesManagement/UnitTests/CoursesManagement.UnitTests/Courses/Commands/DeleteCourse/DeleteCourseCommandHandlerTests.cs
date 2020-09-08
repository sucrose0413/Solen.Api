using System;
using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.DeleteCourse
{
    [TestFixture]
    public class DeleteCourseCommandHandlerTests
    {
        private Mock<IDeleteCourseService> _service;
        private Mock<ICoursesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private DeleteCourseCommand _command;
        private DeleteCourseCommandHandler _sut;
        private Course _courseToDelete;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IDeleteCourseService>();
            _commonService = new Mock<ICoursesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _command = new DeleteCourseCommand {CourseId = "courseId"};
            _sut = new DeleteCourseCommandHandler(_service.Object, _commonService.Object, _unitOfWork.Object,
                _mediator.Object);

            _courseToDelete = new Course("name", "creatorId", DateTime.Now);
            _commonService.Setup(x => x.GetCourseFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToDelete);
        }
        

        [Test]
        public void ControlIsOk_RemoveCourseToDeleteFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveCourseFromRepo(_courseToDelete));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void CourseDeleted_SendCourseDeletedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<CourseDeletedEvent>(),
                default));
        }
        
        [Test]
        public void ModuleDeleted_ReturnCourseDeletedId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_courseToDelete.Id));
        }


        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.RemoveCourseFromRepo(_courseToDelete)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<CourseDeletedEvent>(),
                    default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}