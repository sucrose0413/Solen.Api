using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Lectures;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace CoursesManagement.EventsHandlers.UnitTests.Lectures
{
    [TestFixture]
    public class DeleteLectureResourcesTests
    {
        private DeleteLectureResources _sut;
        private Mock<ILectureResourcesRepo> _repo;
        private Mock<IAppResourceManager> _resourceManager;
        private Mock<IUnitOfWork> _unitOfWork;

        private LectureDeletedEvent _event;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ILectureResourcesRepo>();
            _resourceManager = new Mock<IAppResourceManager>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new DeleteLectureResources(_repo.Object, _resourceManager.Object, _unitOfWork.Object);

            _event = new LectureDeletedEvent("lectureId");
        }

        [Test]
        public void WhenCalled_RemoveLectureResourcesFromRepo()
        {
            var resourcesToDelete = new List<AppResource>
            {
                new AppResource("resourceId1", "organizationId", "creatorName", new ImageType(), 1),
                new AppResource("resourceId2", "organizationId", "creatorName", new ImageType(), 1)
            };

            _repo.Setup(x => x.GetLectureResources(_event.LectureId, default))
                .ReturnsAsync(resourcesToDelete);

            _sut.Handle(_event, default).Wait();

            _resourceManager.Verify(x =>
                    x.Delete(It.IsIn<AppResource>(resourcesToDelete), default),
                Times.Exactly(resourcesToDelete.Count));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            var resourcesToDelete = new List<AppResource>();
            _repo.Setup(x => x.GetLectureResources(_event.LectureId, default))
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
                _repo.Setup(x => x.GetLectureResources(_event.LectureId, default)).InSequence()
                    .ReturnsAsync(resourcesToDelete);
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_event, default).Wait();
            }
        }
    }
}