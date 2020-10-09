using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Courses.Commands
{
    [Binding, Scope(Tag = "PublishCourse")]
    public class PublishCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses/publish";
        private PublishCourseCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;

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
            instructor.AddRoleId(UserRoles.Instructor);
            _instructorId = instructor.Id;
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I publish a course with an empty or a null ""(.*)""")]
        public async Task WhenIPublishACourseWithAnEmptyOrANull(string courseId)
        {
            _command = new PublishCourseCommand {CourseId = courseId};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [When(@"I publish an invalid course Id")]
        public async Task WhenIPublishAnInvalidCourseId()
        {
            _command = new PublishCourseCommand {CourseId = "invalidCourseId"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I publish a course belonging to an other organization")]
        public async Task WhenIPublishACourseBelongingToAnOtherOrganization()
        {
            var otherOrganization = new Organization("other organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(otherOrganization);

            var otherInstructor = new User("otherinstructor@email.com", otherOrganization.Id);
            _factory.CreateUser(otherInstructor);

            var course = new Course("course", otherInstructor.Id, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new PublishCourseCommand {CourseId = course.Id};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Given(@"A draft course that has no module")]
        public void GivenADraftCourseThatHasNoModule()
        {
            var course = CreateDraftCourse();

            _command = new PublishCourseCommand {CourseId = course.Id};
        }

        [Given(@"A draft course that one of its modules has no lecture")]
        public void GivenADraftCourseThatOneOfItsModulesHasNoLecture()
        {
            var course = CreateDraftCourse();

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new PublishCourseCommand {CourseId = course.Id};
        }

        [Given(@"A draft course that one of its articles has no content")]
        public void GivenADraftCourseThatOneOfItsArticlesHasNoContent()
        {
            var course = CreateDraftCourse();

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var article = new ArticleLecture("article", module.Id, 1, content: null);
            _factory.CreateLecture(article);

            _command = new PublishCourseCommand {CourseId = course.Id};
        }

        [Given(@"A draft course that one of its medias has no Url")]
        public void GivenADraftCourseThatOneOfItsMediasHasNoUrl()
        {
            var course = CreateDraftCourse();

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var video = new VideoLecture("video", module.Id, 1);
            _factory.CreateLecture(video);

            _command = new PublishCourseCommand {CourseId = course.Id};
        }

        [Given(@"A draft course with a valid content")]
        public void GivenADraftCourseWithAValidContent()
        {
            var course = CreateDraftCourse();

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);
            
            var article = new ArticleLecture("article", module.Id, 1, content: "some content");
            _factory.CreateLecture(article);

            var video = new VideoLecture("video", module.Id, 1);
            video.SetUrl("mediaUrl");
            _factory.CreateLecture(video);
            
            _command = new PublishCourseCommand {CourseId = course.Id};
        }

        [Then(@"The course should be published")]
        public async Task ThenTheCourseShouldBePublished()
        {
            _response.EnsureSuccessStatusCode();

            var course = await _factory.GetCourseById(_command.CourseId);

            Assert.That(course.CourseStatus.Name, Is.EqualTo(PublishedStatus.Instance.Name));
        }
        
        [When(@"I publish the course")]
        public async Task WhenIPublishTheCourse()
        {
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        #region Private Methods

        private Course CreateDraftCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now.AddDays(-1));
            _factory.CreateCourse(course);
            return course;
        }

        #endregion
    }
}