using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace LearningPaths.Services.UnitTests.Queries.GetLearningPathCourses
{
    [TestFixture]
    public class GetLearningPathCoursesServiceTests
    {
        private Mock<IGetLearningPathCoursesRepository> _repo;
        private GetLearningPathCoursesService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearningPathCoursesRepository>();
            _sut = new GetLearningPathCoursesService(_repo.Object);
        }
        
        [Test]
        public void GetLearningPathCourses_WhenCalled_ReturnTheLearningPathCourses()
        {
            var courses = new List<CourseForLearningPathDto>();
            _repo.Setup(x => x.GetLearningPathCourses("learningPathId", default))
                .ReturnsAsync(courses);

            var result = _sut.GetLearningPathCourses("learningPathId", default).Result;
            
            Assert.That(result, Is.EqualTo(courses));
        }
    }
}