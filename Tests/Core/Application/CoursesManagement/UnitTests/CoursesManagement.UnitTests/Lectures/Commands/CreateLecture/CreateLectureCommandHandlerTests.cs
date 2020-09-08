using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.UnitTests.Lectures.Commands.CreateLecture
{
    [TestFixture]
    public class CreateLectureCommandHandlerTests
    {
        private Mock<ICreateLectureService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private CreateLectureCommand _command;
        private CreateLectureCommandHandler _sut;
        private ArticleLecture _createdLecture;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICreateLectureService>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _command = new CreateLectureCommand();

            _createdLecture = new ArticleLecture(_command.Title, _command.ModuleId, _command.Order, _command.Content);
            _service.Setup(x => x.CreateLecture(_command))
                .Returns(_createdLecture);

            _sut = new CreateLectureCommandHandler(_service.Object, _unitOfWork.Object);
        }

        [Test]
        public void WhenCalled_ControlModuleExistenceAndCourseStatus()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ControlModuleExistenceAndCourseStatus(_command.ModuleId, default));
        }

        [Test]
        public void ControlIsOk_CreateTheLecture()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CreateLecture(_command));
        }

        [Test]
        public void ControlIsOk_UpdateLectureDuration()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateLectureDuration(_createdLecture, _command.Duration));
        }

        [Test]
        public void ControlIsOk_AddLectureToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddLectureToRepo(_createdLecture, default));
        }


        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void SaveChangesIsOk_ReturnCreatedLectureId()
        {
            var result = _sut.Handle(_command, default).Result;

            Assert.That(result.Value, Is.EqualTo(_createdLecture.Id));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.ControlModuleExistenceAndCourseStatus(_command.ModuleId, default))
                    .InSequence();
                _service.Setup(x => x.CreateLecture(_command)).InSequence()
                    .Returns(_createdLecture);
                _service.Setup(x => x.UpdateLectureDuration(_createdLecture, _command.Duration)).InSequence();
                _service.Setup(x => x.AddLectureToRepo(_createdLecture, default)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}