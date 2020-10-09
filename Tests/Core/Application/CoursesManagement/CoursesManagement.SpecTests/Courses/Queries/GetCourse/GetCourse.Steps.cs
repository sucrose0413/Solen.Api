using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Courses.Queries
{
    [Binding, Scope(Tag = "GetCourse")]
    public class GetCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses";

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;
        private string _courseId;
        private Course _courseToGetInfoAbout;

        [BeforeScenario]
        public void ScenarioSetUp()
        {
            _factory = new CoursesManagementTestsFactory();
            _client = _factory.GetAnonymousClient();
        }

        [AfterScenario]
        public void ScenarioTearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [Given(@"I'm an authenticated instructor within the organization")]
        public void GivenImAnAuthenticatedInstructorWithinTheOrganization()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var instructor = new User("instructor@email.com", organization.Id);
            instructor.ChangeUserStatus(ActiveStatus.Instance);
            instructor.UpdateUserName("me");
            instructor.AddRoleId(UserRoles.Instructor);
            _instructorId = instructor.Id;
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I get an invalid course")]
        public async Task WhenIGetAnInvalidCourse()
        {
            _courseId = "invalidCourseId";

            _response = await _client.GetAsync($@"{BaseUrl}/{_courseId}");
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Given(@"A course that has no module")]
        public void GivenACourseThatHasNoModule()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _courseId = course.Id;
            _factory.CreateCourse(course);
        }

        [When(@"I retrieve the course info")]
        public async Task WhenIRetrieveTheCourseInfo()
        {
            _response = await _client.GetAsync($@"{BaseUrl}/{_courseId}");
        }

        [Then(@"The errors list should not be empty")]
        public async Task ThenTheErrorsListShouldNotBeEmpty()
        {
            _response.EnsureSuccessStatusCode();

            var courseViewModel = await Utilities.GetResponseContent<CourseViewModel>(_response);

            Assert.That(courseViewModel.CourseErrors, Is.Not.Empty);
        }

        [Given(@"A course that has no errors")]
        public void GivenACourseThatHasNoErrors()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _courseId = course.Id;
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture = new ArticleLecture("lecture title", module.Id, 1, "article content");
            _factory.CreateLecture(lecture);
        }

        [Then(@"The errors list should be empty")]
        public async Task ThenTheErrorsListShouldBeEmpty()
        {
            _response.EnsureSuccessStatusCode();

            var courseViewModel = await Utilities.GetResponseContent<CourseViewModel>(_response);

            Assert.That(courseViewModel.CourseErrors, Is.Empty);
        }

        [Given(@"An existing training course")]
        public void GivenAnExistingTrainingCourse()
        {
            _courseToGetInfoAbout = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(_courseToGetInfoAbout);
            _courseToGetInfoAbout.AddLearnedSkill("skill1");
            _courseToGetInfoAbout.AddLearnedSkill("skill2");
            _courseId = _courseToGetInfoAbout.Id;

            var module = new Module("module", _courseToGetInfoAbout.Id, 1);
            _factory.CreateModule(module);

            var article = new ArticleLecture("article", module.Id, 1, "article content");
            article.UpdateDuration(60);
            _factory.CreateLecture(article);

            var video = new VideoLecture("video", module.Id, 2);
            video.SetUrl("videoUrl");
            video.UpdateDuration(100);
            _factory.CreateLecture(video);
        }

        [Then(@"The following course info should be correctly returned")]
        public async Task ThenTheFollowingCourseInfoShouldBeCorrectlyReturned(Table table)
        {
            _response.EnsureSuccessStatusCode();

            var courseViewModel = await Utilities.GetResponseContent<CourseViewModel>(_response);
            var retrievedData = courseViewModel.Course;

            Assert.That(retrievedData, Is.Not.Null);
            Assert.That(retrievedData.Id, Is.EqualTo(_courseToGetInfoAbout.Id));
            Assert.That(retrievedData.Title, Is.EqualTo(_courseToGetInfoAbout.Title));
            Assert.That(retrievedData.Subtitle, Is.EqualTo(_courseToGetInfoAbout.Subtitle));
            Assert.That(retrievedData.Creator, Is.EqualTo("me"));
            Assert.That(retrievedData.CreationDate, Is.EqualTo(_courseToGetInfoAbout.CreationDate));
            Assert.That(retrievedData.Status, Is.EqualTo(_courseToGetInfoAbout.CourseStatus.Name));
            Assert.That(retrievedData.Duration, Is.EqualTo(160));
            Assert.That(retrievedData.LectureCount, Is.EqualTo(2));
            Assert.That(retrievedData.CourseLearnedSkills.Count(x => x.Name == "skill1"), Is.EqualTo(1));
            Assert.That(retrievedData.CourseLearnedSkills.Count(x => x.Name == "skill2"), Is.EqualTo(1));
            Assert.That(retrievedData.IsEditable, Is.EqualTo(_courseToGetInfoAbout.IsEditable));
            Assert.That(retrievedData.IsPublished, Is.False);
        }
    }
}