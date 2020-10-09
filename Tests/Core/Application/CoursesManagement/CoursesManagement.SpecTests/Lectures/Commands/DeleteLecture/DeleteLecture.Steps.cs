using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application;
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
    [Binding, Scope(Tag = "DeleteLecture")]
    public class DeleteLectureSteps
    {
        private const string BaseUrl = "/api/courses-management/lectures";

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _lectureToDeleteId;
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

        [When(@"I delete an invalid lecture Id")]
        public async Task WhenIDeleteAnInvalidLectureId()
        {
            _lectureToDeleteId = "invalidLectureId";

            _response = await _client.DeleteAsync($@"{BaseUrl}/{_lectureToDeleteId}");
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I delete a lecture while the course is published")]
        public async Task WhenIDeleteALectureWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture = new ArticleLecture("lecture title", module.Id, 1, "article content");
            _factory.CreateLecture(lecture);

            _response = await _client.DeleteAsync($@"{BaseUrl}/{lecture.Id}");
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Given(@"A lecture belonging to a draft course")]
        public void GivenALectureBelongingToADraftCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture = new ArticleLecture("lecture title", module.Id, 1, "article content");
            _factory.CreateLecture(lecture);

            _lectureToDeleteId = lecture.Id;
        }

        [When(@"I delete the lecture")]
        public async Task WhenIDeleteTheLecture()
        {
            _response = await _client.DeleteAsync($@"{BaseUrl}/{_lectureToDeleteId}");
        }

        [Then(@"The deleted lecture Id should be returned")]
        public async Task ThenTheDeletedLectureIdShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();

            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.EqualTo(_lectureToDeleteId));
        }

        [Then(@"The lecture should be deleted")]
        public async Task ThenTheLectureShouldBeDeleted()
        {
            var deletedLecture = await _factory.GetLectureById(_lectureToDeleteId);

            Assert.That(deletedLecture, Is.Null);
        }
    }
}