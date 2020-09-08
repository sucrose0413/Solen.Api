using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace CoursesManagement.UnitTests.Lectures.Commands.UploadMedia
{
    [TestFixture]
    public class UploadMediaCommandHandlerTests
    {
        private Mock<IUploadMediaService> _lecturesService;
        private Mock<ILecturesCommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;
        private UploadMediaCommand _command;
        private UploadMediaCommandHandler _sut;

        private VideoLecture _mediaLectureToUpdate;
        private ResourceUploadResult _uploadedResource;
        private ResourceToCreate _mediaResource;

        [SetUp]
        public void SetUp()
        {
            _lecturesService = new Mock<IUploadMediaService>();
            _commonService = new Mock<ILecturesCommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _sut = new UploadMediaCommandHandler(_lecturesService.Object, _commonService.Object, _unitOfWork.Object);

            _mediaLectureToUpdate = new VideoLecture("title", "moduleId", 1);
            _command = new UploadMediaCommand(_mediaLectureToUpdate.Id, "lectureType", null);
            _commonService.Setup(x => x.GetLectureFromRepo(_command.LectureId, default))
                .ReturnsAsync(_mediaLectureToUpdate);

            _mediaResource = new ResourceToCreate(_command.File, new VideoType());
            _lecturesService.Setup(x => x.GenerateMediaResource(_command.File))
                .Returns(_mediaResource);
            _uploadedResource = new ResourceUploadResult("resourceId", "resourceUrl");
            _lecturesService.Setup(x => x.UploadResource(_mediaResource))
                .Returns(_uploadedResource);
        }


        [Test]
        public void WhenCalled_CheckCourseStatusForModification()
        {
            _sut.Handle(_command, default).Wait();

            _commonService.Verify(x => x.CheckCourseStatusForModification(_mediaLectureToUpdate));
        }

        [Test]
        public void WhenCalled_CheckIfLectureIsMedia()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.CheckIfTheLectureIsMedia(_mediaLectureToUpdate));
        }

        [Test]
        public void WhenCalled_CheckFileFormat()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.CheckFileFormat(_command.File));
        }

        [Test]
        public void WhenCalled_CheckOrganizationMaximumStorage()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.CheckOrganizationMaximumStorage(_command.File, default));
        }


        [Test]
        public void ControlIsOk_CreateAppResource()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.CreateAppResource(_uploadedResource.ResourceId, _mediaResource));
        }

        [Test]
        public void ControlIsOk_CreateCourseResource()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x =>
                x.CreateCourseResource(_command.LectureId, _uploadedResource.ResourceId, default));
        }

        [Test]
        public void ControlIsOk_SetLectureMediaUrl()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.SetMediaUrl(_mediaLectureToUpdate, _uploadedResource.Url));
        }

        [Test]
        public void ControlIsOk_UpdateLectureRepo()
        {
            _sut.Handle(_command, default).Wait();

            _lecturesService.Verify(x => x.UpdateLectureRepo(_mediaLectureToUpdate));
        }

        [Test]
        public void ControlIsOk_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default), Times.Once);
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _commonService.Setup(x => x.CheckCourseStatusForModification(_mediaLectureToUpdate)).InSequence();
                _lecturesService.Setup(x => x.CheckIfTheLectureIsMedia(_mediaLectureToUpdate)).InSequence();
                _lecturesService.Setup(x => x.CheckFileFormat(_command.File)).InSequence();
                _lecturesService.Setup(x => x.CheckOrganizationMaximumStorage(_command.File, default)).InSequence();
                _lecturesService.Setup(x => x.CreateAppResource(_uploadedResource.ResourceId, _mediaResource)).InSequence();
                _lecturesService.Setup(x =>
                    x.CreateCourseResource(_command.LectureId, _uploadedResource.ResourceId, default)).InSequence();
                _lecturesService.Setup(x => x.SetMediaUrl(_mediaLectureToUpdate, _uploadedResource.Url)).InSequence();
                _lecturesService.Setup(x => x.UpdateLectureRepo(_mediaLectureToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}