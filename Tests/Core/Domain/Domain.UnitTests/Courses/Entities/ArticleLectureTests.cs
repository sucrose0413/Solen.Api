using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using ArticleLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.ArticleLecture;

namespace Domain.UnitTests.Courses.Entities
{
    [TestFixture]
    public class ArticleLectureTests
    {
        private Solen.Core.Domain.Courses.Entities.ArticleLecture _sut;

        [Test]
        public void ConstructorWithTitleModuleIdOrderContent_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new Solen.Core.Domain.Courses.Entities.ArticleLecture("title", "moduleId", 1, "content");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Title, Is.EqualTo("title"));
            Assert.That(_sut.ModuleId, Is.EqualTo("moduleId"));
            Assert.That(_sut.Order, Is.EqualTo(1));
            Assert.That(_sut.Content, Is.EqualTo("content"));
            Assert.That(_sut.LectureType, Is.TypeOf<ArticleLecture>());
        }

        [Test]
        public void ConstructorWithTitleModuleOrderContent_WhenCalled_SetPropertiesCorrectly()
        {
            var module = new Module("module", "courseId", 1);
            _sut = new Solen.Core.Domain.Courses.Entities.ArticleLecture("title", module, 1, "content");

            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Title, Is.EqualTo("title"));
            Assert.That(_sut.ModuleId, Is.EqualTo(module.Id));
            Assert.That(_sut.Module, Is.EqualTo(module));
            Assert.That(_sut.Order, Is.EqualTo(1));
            Assert.That(_sut.Content, Is.EqualTo("content"));
            Assert.That(_sut.LectureType, Is.TypeOf<ArticleLecture>());
        }

        [Test]
        public void UpdateContent_WhenCalled_UpdateArticleContent()
        {
            _sut = new Solen.Core.Domain.Courses.Entities.ArticleLecture("title", "moduleId", 1, " old content");

            _sut.UpdateContent("new content");

            Assert.That(_sut.Content, Is.EqualTo("new content"));
        }
    }
}