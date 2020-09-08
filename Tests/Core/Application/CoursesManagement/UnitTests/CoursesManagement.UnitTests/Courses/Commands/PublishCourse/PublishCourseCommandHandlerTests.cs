using System;
using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.PublishCourse
{
    [TestFixture]
    public class PublishCourseCommandHandlerTests
    {
        private Mock<IPublishCourseService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private PublishCourseCommand _command;
        private PublishCourseCommandHandler _sut;
        private Course _courseToPublish;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IPublishCourseService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _command = new PublishCourseCommand {CourseId = "courseId"};
            _sut = new PublishCourseCommandHandler(_service.Object, _unitOfWork.Object, _mediator.Object);

            _courseToPublish = new Course("name", "creatorId", DateTime.Now);
            _service.Setup(x => x.GetCourseWithDetailsFromRepo(_command.CourseId, default))
                .ReturnsAsync(_courseToPublish);
        }

        [Test]
        public void WhenCall_CheckCourseErrors()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckCourseErrors(_command.CourseId, default));
        }

        [Test]
        public void ControlIsOk_ChangeCourseStatusToPublished()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ChangeTheCourseStatusToPublished(_courseToPublish));
        }


        [Test]
        public void ControlIsOk_UpdateCoursePublicationDate()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdatePublicationDate(_courseToPublish));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void ControlIsOk_UpdateCourseRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateCourseRepo(_courseToPublish));
        }

        [Test]
        public void ChangesSaved_PublishCoursePublishedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<CoursePublishedEventNotification>(),
                default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckCourseErrors(_command.CourseId, default)).InSequence();
                _service.Setup(x => x.ChangeTheCourseStatusToPublished(_courseToPublish)).InSequence();
                _service.Setup(x => x.UpdatePublicationDate(_courseToPublish)).InSequence();
                _service.Setup(x => x.UpdateCourseRepo(_courseToPublish)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<CoursePublishedEventNotification>(),
                    default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}