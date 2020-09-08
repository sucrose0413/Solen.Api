using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Lectures.Commands
{
    [Binding, Scope(Tag = "UpdateLecture")]
    public class UpdateLectureSteps
    {
        private const string BaseUrl = "/api/courses-management/lectures";
        private UpdateLectureCommand _command;

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
            instructor.ChangeUserStatus(new ActiveStatus());
            instructor.AddRoleId(UserRoles.Instructor);
            _instructorId = instructor.Id;
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I update a lecture with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateALectureWithAnEmptyOrANullId(string lectureId)
        {
            _command = new UpdateLectureCommand {LectureId = lectureId, Title = "lecture title"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a lecture with an empty or a null title ""(.*)""")]
        public async Task WhenIUpdateALectureWithAnEmptyOrANullTitle(string title)
        {
            _command = new UpdateLectureCommand {LectureId = "lectureId", Title = title};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a lecture with a title length over (.*) characters")]
        public async Task WhenIUpdateALectureWithATitleLengthOverCharacters(int maximumTitleLength)
        {
            _command = new UpdateLectureCommand
                {LectureId = "lectureId", Title = new string('*', maximumTitleLength + 1)};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update an invalid lecture Id")]
        public async Task WhenIUpdateAnInvalidLectureId()
        {
            _command = new UpdateLectureCommand {LectureId = "invalidLectureId", Title = "lecture title"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update a lecture while the course is published")]
        public async Task WhenIUpdateALectureWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(new PublishedStatus());
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture = new ArticleLecture("lecture title", module.Id, 1, "article content");
            _factory.CreateLecture(lecture);

            _command = new UpdateLectureCommand {LectureId = lecture.Id, Title = "lecture title"};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A lecture belonging to a draft course with")]
        public void GivenALectureBelongingToADraftCourseWith(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture = new ArticleLecture("old title", module.Id, 1, "old content");
            lecture.UpdateDuration(60);
            _factory.CreateLecture(lecture);

            _command = new UpdateLectureCommand {LectureId = lecture.Id};
        }

        [When(@"I update the lecture with")]
        public async Task WhenIUpdateTheLectureWith(Table table)
        {
            _command.Title = "new title";
            _command.Content = "new content";
            _command.Duration = 160;

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The lecture should be successfully updated with the new data")]
        public async Task ThenTheLectureShouldBeSuccessfullyUpdatedWithTheNewData()
        {
            _response.EnsureSuccessStatusCode();
            
            var lecture = await _factory.GetLectureById(_command.LectureId);
            
            Assert.That(lecture.Title, Is.EqualTo(_command.Title));
            Assert.That(lecture.Duration, Is.EqualTo(_command.Duration));
            Assert.That((lecture as ArticleLecture)?.Content, Is.EqualTo(_command.Content));
        }
    }
}