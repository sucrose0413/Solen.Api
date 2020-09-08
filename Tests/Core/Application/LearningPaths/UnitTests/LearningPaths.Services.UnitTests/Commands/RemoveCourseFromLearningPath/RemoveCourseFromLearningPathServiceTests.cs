using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.Services.UnitTests.Commands.RemoveCourseFromLearningPath
{
    [TestFixture]
    public class RemoveCourseFromLearningPathServiceTests
    {
        private Mock<IRemoveCourseFromLearningPathRepository> _repo;
        private RemoveCourseFromLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IRemoveCourseFromLearningPathRepository>();
            _sut = new RemoveCourseFromLearningPathService(_repo.Object);
        }

        [Test]
        public void GetLearningPathCourseFromRepo_WhenCalled_ReturnLearningPathCourse()
        {
            var learningPathCourse = new LearningPathCourse("learningPathId", "courseId", 1);
            _repo.Setup(x => x.GetLearningPathCourse("learningPathId", "courseId", default))
                .ReturnsAsync(learningPathCourse);

            var result = _sut.GetLearningPathCourseFromRepo("learningPathId", "courseId", default).Result;

            Assert.That(result, Is.EqualTo(learningPathCourse));
        }
        
        [Test]
        public void RemoveLearningPathCourseFromRepo_WhenCalled_RemoveLearningPathCourseFromRepo()
        {
            var learningPathCourse = new LearningPathCourse("learningPathId", "courseId", 1);

            _sut.RemoveLearningPathCourseFromRepo(learningPathCourse);

           _repo.Verify(x => x.RemoveLearningPathCourse(learningPathCourse));
        }
    }
}