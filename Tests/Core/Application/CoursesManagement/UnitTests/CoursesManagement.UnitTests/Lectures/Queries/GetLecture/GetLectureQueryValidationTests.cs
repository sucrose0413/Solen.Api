using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Lectures.Queries;

namespace CoursesManagement.UnitTests.Lectures.Queries.GetLecture
{
    [TestFixture]
    public class GetLectureQueryValidationTests
    {
        private GetLectureQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetLectureQueryValidator();
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NullOrEmptyLectureId_ShouldHaveError(string lectureId)
        {
            var query = new GetLectureQuery { LectureId = lectureId};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureId, query);
        }
        
        [Test]
        public void ValidLectureId_ShouldNotHaveError()
        {
            var query = new GetLectureQuery { LectureId = "lectureId"};  

            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureId, query);
        }
    }
}