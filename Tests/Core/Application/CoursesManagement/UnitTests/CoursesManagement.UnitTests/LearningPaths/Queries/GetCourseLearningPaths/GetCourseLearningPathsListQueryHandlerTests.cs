using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.LearningPaths.Queries;

namespace CoursesManagement.UnitTests.LearningPaths.Queries.GetCourseLearningPaths
{
    [TestFixture]
    public class GetCourseLearningPathsListQueryHandlerTests
    {
        private Mock<IGetCourseLearningPathsService> _service;
        private GetCourseLearningPathsListQueryHandler _sut;
        private GetCourseLearningPathsListQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCourseLearningPathsService>();
            _sut = new GetCourseLearningPathsListQueryHandler(_service.Object);
            _query = new GetCourseLearningPathsListQuery();
        }


        [Test]
        public void WhenCalled_ReturnCorrectOrganizationLearningPaths()
        {
            var organizationLearningPaths = new List<CourseLearningPathDto>();
            _service.Setup(x => x.GetOrganizationLearningPaths(default))
                .ReturnsAsync(organizationLearningPaths);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearningPaths, Is.EqualTo(organizationLearningPaths));
        }


        [Test]
        public void WhenCalled_ReturnCorrectCourseLearningPathsIds()
        {
            var courseLearningPathIds = new List<string>();
            _service.Setup(x => x.GetCourseLearningPathsIds(_query.CourseId, default))
                .ReturnsAsync(courseLearningPathIds);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseLearningPathsIds, Is.EquivalentTo(courseLearningPathIds));
        }
    }
}