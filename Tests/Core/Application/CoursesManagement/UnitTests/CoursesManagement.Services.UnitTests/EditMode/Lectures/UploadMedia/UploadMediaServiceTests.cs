using System;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;
using Solen.Core.Domain.Subscription.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Lectures
{
    [TestFixture]
    public class UploadMediaServiceTests
    {
        private Mock<IUploadMediaRepository> _repo;
        private Mock<IOrganizationSubscriptionManager> _subscriptionManager;
        private Mock<IAppResourceManager> _appResourceManager;
        private UploadMediaService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUploadMediaRepository>();
            _subscriptionManager = new Mock<IOrganizationSubscriptionManager>();
            _appResourceManager = new Mock<IAppResourceManager>();
            _sut = new UploadMediaService(_repo.Object, _subscriptionManager.Object, _appResourceManager.Object);

            var subscription = new SubscriptionPlan("id", "subscription", 20, 1, 10);
            _subscriptionManager.Setup(x => x.GetOrganizationSubscriptionPlan(default)).ReturnsAsync(subscription);
        }

        [Test]
        public void CheckIfTheLectureIsMedia_LectureIsNotAMedia_ThrowAppBusinessException()
        {
            var lecture = new ArticleLecture("lecture", "module1", 1, "content");

            Assert.That(() => _sut.CheckIfTheLectureIsMedia(lecture),
                Throws.Exception.TypeOf<AppBusinessException>());
        }

        [Test]
        public void CheckIfTheLectureIsMedia_LectureIsAMedia_NotThrowException()
        {
            var lecture = new VideoLecture("lecture", "module1", 1);

            Assert.That(() => _sut.CheckIfTheLectureIsMedia(lecture), Throws.Nothing);
        }


        [Test]
        public void CheckFileFormat_NotVideoMp4Format_ThrowUnsupportedFileFormatException()
        {
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.ContentType).Returns("file/type");

            Assert.That(() => _sut.CheckFileFormat(file.Object),
                Throws.Exception.TypeOf<UnsupportedFileFormatException>());
        }

        [Test]
        public void CheckFileFormat_VideoMp4Format_NotThrowException()
        {
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.ContentType).Returns("video/mp4");

            Assert.That(() => _sut.CheckFileFormat(file.Object), Throws.Nothing);
        }

        [Test]
        public void
            CheckOrganizationMaximumStorage_FileDoesExceedTotalMaximumStorage_ThrowFileExceedsTotalMaximumStorageException()
        {
            //--- Arrange
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.Length).Returns(1);
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default)).ReturnsAsync(20);

            // Act & Assert
            Assert.That(() => _sut.CheckOrganizationMaximumStorage(file.Object, default),
                Throws.Exception.TypeOf<FileExceedsTotalMaximumStorageException>());
        }


        [Test]
        public void CheckOrganizationMaximumStorage_FileDoesNotExceedTotalMaximumStorage_NotThrowException()
        {
            // Arrange
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.Length).Returns(1);
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default)).ReturnsAsync(10);


            // Act & Assert
            Assert.That(() => _sut.CheckOrganizationMaximumStorage(file.Object, default), Throws.Nothing);
        }

        [Test]
        public void
            CheckOrganizationMaximumStorage_FileDoesExceedMaximumFileSize_ThrowFileExceedsMaximumFileSizeException()
        {
            //--- Arrange
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.Length).Returns(2);
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default)).ReturnsAsync(10);

            // Act & Assert
            Assert.That(() => _sut.CheckOrganizationMaximumStorage(file.Object, default),
                Throws.Exception.TypeOf<FileExceedsMaximumFileSizeException>());
        }


        [Test]
        public void CheckOrganizationMaximumStorage_FileDoesNotExceedMaximumFileSize_NotThrowException()
        {
            // Arrange
            var file = new Mock<IResourceFile>();
            file.Setup(x => x.Length).Returns(1);
            _subscriptionManager.Setup(x => x.GetOrganizationCurrentStorage(default)).ReturnsAsync(10);


            // Act & Assert
            Assert.That(() => _sut.CheckOrganizationMaximumStorage(file.Object, default), Throws.Nothing);
        }

        [Test]
        public void GenerateMediaResource_WhenCalled_GenerateTheCorrectMediaResource()
        {
            var file = new Mock<IResourceFile>();

            var result = _sut.GenerateMediaResource(file.Object);

            Assert.That(result.File, Is.EqualTo(file.Object));
            Assert.That(result.ResourceType.Name, Is.EqualTo(new VideoType().Name));
        }

        [Test]
        public void UploadResource_WhenCalled_UploadResource()
        {
            // Arrange
            var file = new Mock<IResourceFile>();
            var resourceToCreate = new ResourceToCreate(file.Object, new VideoType());
            var uploadResult = new ResourceUploadResult("resourceId", "resourceUrl");
            _appResourceManager.Setup(x => x.UploadResource(resourceToCreate))
                .Returns(uploadResult);

            // Act
            var result = _sut.UploadResource(resourceToCreate);

            // Assert
            Assert.That(result, Is.EqualTo(uploadResult));
        }

        [Test]
        public void CreateAppResource_WhenCalled_CreateAppResource()
        {
            // Arrange
            var file = new Mock<IResourceFile>();
            var resourceToCreate = new ResourceToCreate(file.Object, new VideoType());
            var appResource = new AppResource("resourceId", "organizationId", "creator", new VideoType(), 1);
            _appResourceManager.Setup(x => x.CreateAppResource("resourceId", resourceToCreate))
                .Returns(appResource);

            // Act
            _sut.CreateAppResource("resourceId", resourceToCreate);

            // Assert
            _appResourceManager.Verify(x => x.AddAppResourceToRepo(appResource));
        }

        [Test]
        public void CreateCourseResource_WhenCalled_CreateCourseResource()
        {
            // Arrange
            var moduleIdCourseId = new LectureModuleIdCourseId("moduleId", "courseId");
            _repo.Setup(x => x.GetLectureModuleIdAndCourseId("lectureId", default))
                .ReturnsAsync(moduleIdCourseId);

            // Act
            _sut.CreateCourseResource("lectureId", "resourceId", default).Wait();

            // Assert
            _repo.Verify(x => x.AddCourseResource(It.Is(CourseResourceMatch()),
                default));
        }

        [Test]
        public void SetMediaUrl_WhenCalled_SetMediaUrl()
        {
            var media = new Mock<MediaLecture>("title", "moduleId", new Solen.Core.Domain.Courses.Enums.LectureTypes.VideoLecture(), 1);

            _sut.SetMediaUrl(media.Object, "url");

            media.Verify(x => x.SetUrl("url"));
        }

        [Test]
        public void UpdateLectureRepo_WhenCalled_UpdateLectureRepo()
        {
            var lecture = new VideoLecture("title", "moduleId", 1);

            _sut.UpdateLectureRepo(lecture);

            _repo.Verify(x => x.UpdateLecture(lecture));
        }

        #region Private Methods

        private static Expression<Func<CourseResource, bool>> CourseResourceMatch()
        {
            return c => c.ResourceId == "resourceId" && c.ModuleId == "moduleId" && c.LectureId == "lectureId" &&
                        c.CourseId == "courseId";
        }

        #endregion
    }
}