using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using ArticleLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.ArticleLecture;

namespace CoursesManagement.Factories.UnitTests.LectureCreation
{
    [TestFixture]
    public class ArticleCreatorTests
    {
        private CreateLectureCommand _command;
        private ArticleCreator _sut;

        [SetUp]
        public void SetUp()
        {
            _command = new CreateLectureCommand
                {Title = "title", ModuleId = "moduleId", Order = 1, Content = "Content"};
            _sut = new ArticleCreator();
        }

        [Test]
        public void Create_WhenCalled_CreateArticleLecture()
        {
            var result = _sut.Create(_command);

            Assert.That(result, Is.TypeOf<Solen.Core.Domain.Courses.Entities.ArticleLecture>());
        }

        [Test]
        public void Create_WhenCalled_CreateArticleWithCorrectData()
        {
            var result = (Solen.Core.Domain.Courses.Entities.ArticleLecture) _sut.Create(_command);

            Assert.That(result.Title, Is.EqualTo(_command.Title));
            Assert.That(result.ModuleId, Is.EqualTo(_command.ModuleId));
            Assert.That(result.Order, Is.EqualTo(_command.Order));
            Assert.That(result.Content, Is.EqualTo(_command.Content));
        }

        [Test]
        public void LectureType_WhenCalled_CreateArticleType()
        {
            var result = _sut.LectureType;

            Assert.That(result, Is.TypeOf<ArticleLecture>());
        }
    }
}