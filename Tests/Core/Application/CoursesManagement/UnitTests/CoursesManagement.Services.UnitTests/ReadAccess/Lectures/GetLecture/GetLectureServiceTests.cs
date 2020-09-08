using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Services.Lectures;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Lectures
{
    [TestFixture]
    public class GetLectureServiceTests
    {
        private Mock<IGetLectureRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetLectureService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLectureRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetLectureService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLecture_LectureDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLecture("lectureId", "organizationId", default))
                .ReturnsAsync((LectureDto) null);

            Assert.That(() => _sut.GetLecture("lectureId", default), Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetLecture_LectureDoesExist_ReturnCorrectLecture()
        {
            var lecture = new LectureDto();
            _repo.Setup(x => x.GetLecture("lectureId", "organizationId", default))
                .ReturnsAsync(lecture);

            var result = _sut.GetLecture("lectureId", default).Result;

            Assert.That(result, Is.EqualTo(lecture));
        }
    }
}