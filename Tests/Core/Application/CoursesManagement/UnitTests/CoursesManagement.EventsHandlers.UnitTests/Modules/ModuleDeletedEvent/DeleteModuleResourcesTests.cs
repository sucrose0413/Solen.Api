using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Modules;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace CoursesManagement.EventsHandlers.UnitTests.Modules
{
    [TestFixture]
    public class DeleteModuleResourcesTests
    {
        private DeleteModuleResources _sut;
        private Mock<IModuleResourcesRepo> _repo;
        private Mock<IAppResourceManager> _resourceManager;
        private Mock<IUnitOfWork> _unitOfWork;

        private ModuleDeletedEvent _event;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IModuleResourcesRepo>();
            _resourceManager = new Mock<IAppResourceManager>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new DeleteModuleResources(_repo.Object, _resourceManager.Object, _unitOfWork.Object);

            _event = new ModuleDeletedEvent("moduleIds");
        }

        [Test]
        public void WhenCalled_RemoveModuleResourcesFromRepo()
        {
            var resourcesToDelete = new List<AppResource>();
            _repo.Setup(x => x.GetModuleResources(_event.ModuleId, default))
                .ReturnsAsync(resourcesToDelete);

            _sut.Handle(_event, default).Wait();

            _resourceManager.Verify(x =>
                    x.Delete(It.IsIn<AppResource>(resourcesToDelete), default),
                Times.Exactly(resourcesToDelete.Count));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            var resourcesToDelete = new List<AppResource>
            {
                new AppResource("resourceId1", "organizationId", "creatorName", new ImageType(), 1),
                new AppResource("resourceId2", "organizationId", "creatorName", new ImageType(), 1)
            };
            _repo.Setup(x => x.GetModuleResources(_event.ModuleId, default))
                .ReturnsAsync(resourcesToDelete);

            _sut.Handle(_event, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                var resourcesToDelete = new List<AppResource>();
                _repo.Setup(x => x.GetModuleResources(_event.ModuleId, default)).InSequence()
                    .ReturnsAsync(resourcesToDelete);
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_event, default).Wait();
            }
        }
    }
}