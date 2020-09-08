using System;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Courses.Commands.CreateCourse
{
    [TestFixture]
    public class CreateCourseCommandHandlerTests
    {
        private Mock<ICreateCourseService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private CreateCourseCommand _command;
        private CreateCourseCommandHandler _sut;

        private Course _createdCourse;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICreateCourseService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _command = new CreateCourseCommand {Title = "course title"};

            _sut = new CreateCourseCommandHandler(_service.Object, _unitOfWork.Object);

            _createdCourse = new Course(_command.Title, "creatorId", DateTime.Now);
            _service.Setup(c => c.CreateCourse(_command.Title)).Returns(_createdCourse);
        }

        [Test]
        public void WhenCalled_CreateTheCourse()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CreateCourse(_command.Title));
        }

        [Test]
        public void WhenCalled_AddCourseToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddCourseToRepo(_createdCourse, default));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_ReturnCreatedCourseId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_createdCourse.Id));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CreateCourse(_command.Title)).InSequence()
                    .Returns(_createdCourse);
                _service.Setup(x => x.AddCourseToRepo(_createdCourse, default)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}