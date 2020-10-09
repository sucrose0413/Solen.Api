using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class CourseTests
    {
        private Mock<IDateTime> _dateTime;
        private Course _sut;

        [SetUp]
        public void SetUp()
        {
            _dateTime = new Mock<IDateTime>();
         
            _sut = new Course("title", "creatorId", DateTime.Now);
        }

        [Test]
        public void ConstructorWithTitleCreatorId_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            var now = _dateTime.Object.Now;
            _sut = new Course("title", "creatorId", now);

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Title, Is.EqualTo("title"));
            Assert.That(_sut.CreatorId, Is.EqualTo("creatorId"));
            Assert.That(_sut.CreationDate, Is.EqualTo(now));
            Assert.That(_sut.CourseStatusName, Is.EqualTo(DraftStatus.Instance.Name));
        }

        [Test]
        public void UpdateTitle_WhenCalled_UpdateCourseTitle()
        {
            _sut.UpdateTitle("new title");

            Assert.That(_sut.Title, Is.EqualTo("new title"));
        }

        [Test]
        public void UpdateSubtitle_WhenCalled_UpdateCourseSubtitle()
        {
            _sut.UpdateSubtitle("new Subtitle");

            Assert.That(_sut.Subtitle, Is.EqualTo("new Subtitle"));
        }

        [Test]
        public void UpdateDescription_WhenCalled_UpdateCourseDescription()
        {
            _sut.UpdateDescription("new description");

            Assert.That(_sut.Description, Is.EqualTo("new description"));
        }

        [Test]
        public void UpdatePublicationDate_WhenCalled_UpdateCoursePublicationDate()
        {
            var publicationDate = DateTime.Now;

            _sut.UpdatePublicationDate(publicationDate);

            Assert.That(_sut.PublicationDate, Is.EqualTo(publicationDate));
        }

        [Test]
        public void ChangeCourseStatus_WhenCalled_UpdateCourseStatus()
        {
            var publishedStatus = PublishedStatus.Instance;
            
            _sut.ChangeCourseStatus(publishedStatus);

            Assert.That(_sut.CourseStatusName, Is.EqualTo(publishedStatus.Name));
        }

        [Test]
        public void AddLearningPath_WhenCalled_AddTheLearningPathToTheCourseLearningPathsList()
        {
            var learningPath = new LearningPathCourse("learningPathId", "courseId", 1);

            _sut.AddLearningPath(learningPath);

            Assert.That(_sut.CourseLearningPaths, Has.Member(learningPath));
        }

        [Test]
        public void RemoveLearningPath_WhenCalled_RemoveTheLearningPathFromTheCourseLearningPathsList()
        {
            var learningPath = new LearningPathCourse("learningPathId", "courseId", 1);
            _sut.AddLearningPath(learningPath);

            _sut.RemoveLearningPath(learningPath);

            Assert.That(_sut.CourseLearningPaths, Has.No.Member(learningPath));
        }

        [Test]
        public void IsEditable_CourseStatusIsDraft_ReturnTrue()
        {
            _sut.ChangeCourseStatus(DraftStatus.Instance);
            
            var result = _sut.IsEditable;

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsEditable_CourseStatusIsUnpublished_ReturnTrue()
        {
            _sut.ChangeCourseStatus(UnpublishedStatus.Instance);
            
            var result = _sut.IsEditable;

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsEditable_CourseStatusIsPublished_ReturnFalse()
        {
            _sut.ChangeCourseStatus(PublishedStatus.Instance);
            
            var result = _sut.IsEditable;

            Assert.That(result, Is.False);
        }
    }
}