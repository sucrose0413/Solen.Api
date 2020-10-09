using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Common.Resources.UnitTests
{
    [TestFixture]
    public class AppResourceManagerTests
    {
        private AppResourceManager _sut;
        private Mock<IAppResourceManagerRepo> _repo;
        private Mock<IResourceStorageManagerFactory> _storageFactory;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        private ResourceToCreate _resourceToCreate;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IAppResourceManagerRepo>();
            _storageFactory = new Mock<IResourceStorageManagerFactory>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new AppResourceManager(_repo.Object, _storageFactory.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId)
                .Returns("organizationId");
            _currentUserAccessor.Setup(x => x.Username)
                .Returns("creatorName");

            var file = new Mock<IResourceFile>();
            _resourceToCreate = new ResourceToCreate(file.Object, VideoType.Instance);
        }

        [Test]
        public void UploadResource_WhenCalled_UploadResource()
        {
            var resourceAccessor = new Mock<ResourceAccessor>();
            _storageFactory.Setup(x => x.Create(_resourceToCreate.ResourceType))
                .Returns(resourceAccessor.Object);

            _sut.UploadResource(_resourceToCreate);

            resourceAccessor.Verify(x => x.Add(_resourceToCreate.File));
        }

        [Test]
        public void CreateAppResource_WhenCalled_CreateAppResource()
        {
            var result = _sut.CreateAppResource("resourceId", _resourceToCreate);

            Assert.That(result.Id, Is.EqualTo("resourceId"));
            Assert.That(result.OrganizationId, Is.EqualTo("organizationId"));
            Assert.That(result.CreatorName, Is.EqualTo("creatorName"));
            Assert.That(result.ResourceType, Is.EqualTo(_resourceToCreate.ResourceType));
            Assert.That(result.Size, Is.EqualTo(_resourceToCreate.File.Length));
        }

        [Test]
        public void AddAppResourceToRepo_WhenCalled_AddAppResourceToRepo()
        {
            var appResource = new AppResource("resourceId", "organizationId", "creatorName",
                _resourceToCreate.ResourceType, _resourceToCreate.File.Length);

            _sut.AddAppResourceToRepo(appResource);

            _repo.Verify(x => x.AddResource(appResource));
        }

        [Test]
        public void Delete_WhenCalled_MarkAppResourceAsDeleted()
        {
            var appResource = new Mock<AppResource>("resourceId", "organizationId", "creatorName",
                _resourceToCreate.ResourceType, _resourceToCreate.File.Length);

            _sut.Delete(appResource.Object, default);

            appResource.Verify(x => x.MarkToDelete());
        }

        [Test]
        public void Delete_WhenCalled_UpdateAppResourceRepo()
        {
            var appResource = new AppResource("resourceId", "organizationId", "creatorName",
                _resourceToCreate.ResourceType, _resourceToCreate.File.Length);

            _sut.Delete(appResource, default);

            _repo.Verify(x => x.UpdateResource(appResource));
        }
    }
}