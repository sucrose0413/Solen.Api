using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Lectures
{
    [TestFixture]
    public class DeleteLectureServiceTests
    {
        private Mock<IDeleteLectureRepository> _repo;
        private DeleteLectureService _sut;
        
        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IDeleteLectureRepository>();
            _sut = new DeleteLectureService(_repo.Object);
        }
        
        [Test]
        public void RemoveLectureFromRepo_WhenCalled_RemoveLectureFromRepo()
        {
            var lecture = new ArticleLecture("lecture", "moduleId", 1, "content");

            _sut.RemoveLectureFromRepo(lecture);

            _repo.Verify(x => x.RemoveLecture(lecture));
        }
    }
}