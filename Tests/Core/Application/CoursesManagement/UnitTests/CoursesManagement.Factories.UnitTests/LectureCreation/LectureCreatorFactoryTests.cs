using System.Collections.Generic;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using ArticleLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.ArticleLecture;
using VideoLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.VideoLecture;

namespace CoursesManagement.Factories.UnitTests.LectureCreation
{
    [TestFixture]
    public class LectureCreatorFactoryTests
    {
        private CreateLectureCommand _command;
        private LectureCreatorFactory _sut;

        [SetUp]
        public void SetUp()
        {
            _command = new CreateLectureCommand
                {Title = "title", ModuleId = "moduleId", Order = 1, Content = "Content"};
            _sut = new LectureCreatorFactory(null);
        }

        [Test]
        public void Create_NoLectureCreators_ThrowLectureCreatorNotFoundException()
        {
            _command.LectureType = "Article";

            Assert.That(() => _sut.Create(_command),
                Throws.Exception.TypeOf<LectureCreatorNotFoundException>());
        }

        [Test]
        public void Create_LectureTypeIsArticle_CreateArticleLecture()
        {
            _command.LectureType = ArticleLecture.Instance.Name;
            _sut = new LectureCreatorFactory(new List<ILectureCreator> {new ArticleCreator(), new VideoCreator()});

            var result = _sut.Create(_command);

            Assert.That(result, Is.TypeOf<Solen.Core.Domain.Courses.Entities.ArticleLecture>());
        }

        [Test]
        public void Create_LectureTypeIsVideo_CreateVideoLecture()
        {
            _command.LectureType = VideoLecture.Instance.Name;
            _sut = new LectureCreatorFactory(new List<ILectureCreator> {new VideoCreator(), new ArticleCreator()});

            var result = _sut.Create(_command);

            Assert.That(result, Is.TypeOf<Solen.Core.Domain.Courses.Entities.VideoLecture>());
        }
    }
}