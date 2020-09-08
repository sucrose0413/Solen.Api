using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace LearningPaths.Services.UnitTests.Commands.AddCoursesToLearningPath
{
    [TestFixture]
    public class AddCoursesToLearningPathServiceTests
    {
        private Mock<IAddCoursesToLearningPathRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private AddCoursesToLearningPathService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IAddCoursesToLearningPathRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new AddCoursesToLearningPathService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLearningPathFromRepo_LearningPathDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLearningPathWithCourses("learningPathId", "organizationId", default))
                .ReturnsAsync((LearningPath) null);

            Assert.That(() => _sut.GetLearningPathFromRepo("learningPathId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void GetLearningPathFromRepo_LearningPathDoesExist_ReturnTheCorrectLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _repo.Setup(x => x.GetLearningPathWithCourses("learningPathId", "organizationId", default))
                .ReturnsAsync(learningPath);

            var result = _sut.GetLearningPathFromRepo("learningPathId", default).Result;
            
            Assert.That(result, Is.EqualTo(learningPath));
        }
        
        [Test]
        public void DoesCourseExist_CourseDoesNotExist_ReturnFalse()
        {
            _repo.Setup(x => x.DoesCourseExist("courseId", "organizationId", default))
                .ReturnsAsync(false);

            var result = _sut.DoesCourseExist("courseId", default).Result;
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void DoesCourseExist_CourseDoestExist_ReturnTrue()
        {
            _repo.Setup(x => x.DoesCourseExist("courseId", "organizationId", default))
                .ReturnsAsync(true);

            var result = _sut.DoesCourseExist("courseId", default).Result;
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void AddCourseToLearningPath_WhenCalled_AddCourseToLearningPath()
        {
            var learningPath = new Mock<LearningPath>("name", "organizationId", true);

           _sut.AddCourseToLearningPath(learningPath.Object, "courseId");
            
            learningPath.Verify(x => x.AddCourse("courseId"));
        }
        
        [Test]
        public void UpdateLearningPathRepo_WhenCalled_UpdateLearningPathRepo()
        {
            var learningPath = new LearningPath("name", "organizationId");
  
            _sut.UpdateLearningPathRepo(learningPath);
            
            _repo.Verify(x => x.UpdateLearningPath(learningPath));
        }
    }
}