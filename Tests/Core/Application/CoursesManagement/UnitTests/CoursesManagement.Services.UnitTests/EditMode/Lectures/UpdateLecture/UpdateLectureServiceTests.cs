using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Lectures
{
    [TestFixture]
    public class UpdateLectureServiceTests
    {
        private Mock<IUpdateLectureRepository> _repo;
        private UpdateLectureService _sut;
        
        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateLectureRepository>();
            _sut = new UpdateLectureService(_repo.Object);
        }
        
        [Test]
        public void UpdateLectureRepo_WhenCalled_UpdateLectureRepo()
        {
            var lecture = new ArticleLecture("lecture", "moduleId", 1, "content");

            _sut.UpdateLectureRepo(lecture);

            _repo.Verify(x => x.UpdateLecture(lecture));
        }
    }
}