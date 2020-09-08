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
    [Binding, Scope(Tag = "UnpublishCourse")]
    public class UnpublishCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses/unpublish";
        private UnpublishCourseCommand _command;

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

        [When(@"I unpublish a course with an empty or a null ""(.*)""")]
        public async Task WhenIUnpublishACourseWithAnEmptyOrANull(string courseId)
        {
            _command = new UnpublishCourseCommand {CourseId = courseId};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I unpublish an invalid course Id")]
        public async Task WhenIUnpublishAnInvalidCourseId()
        {
            _command = new UnpublishCourseCommand {CourseId = "invalidCourseId"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I unpublish a course belonging to an other organization")]
        public async Task WhenIUnpublishACourseBelongingToAnOtherOrganization()
        {
            var otherOrganization = new Organization("other organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(otherOrganization);

            var otherInstructor = new User("otherinstructor@email.com", otherOrganization.Id);
            _factory.CreateUser(otherInstructor);

            var course = new Course("course", otherInstructor.Id, DateTime.Now);
            course.ChangeCourseStatus(new UnpublishedStatus());
            _factory.CreateCourse(course);

            _command = new UnpublishCourseCommand {CourseId = course.Id};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Given(@"A existing published course")]
        public void GivenAExistingPublishedCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now.AddDays(-1));
            course.ChangeCourseStatus(new PublishedStatus());
            _factory.CreateCourse(course);

            _command = new UnpublishCourseCommand {CourseId = course.Id};
        }

        [When(@"I unpublish the course")]
        public async Task WhenIUnpublishTheCourse()
        {
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }


        [Then(@"The course should be unpublished")]
        public async Task ThenTheCourseShouldBeUnpublished()
        {
            _response.EnsureSuccessStatusCode();

            var course = await _factory.GetCourseById(_command.CourseId);

            Assert.That(course.CourseStatus.Name, Is.EqualTo(new UnpublishedStatus().Name));
        }
    }
}