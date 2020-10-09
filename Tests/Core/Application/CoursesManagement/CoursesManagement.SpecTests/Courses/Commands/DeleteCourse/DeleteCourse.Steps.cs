using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Courses.Commands
{
    [Binding, Scope(Tag = "DeleteCourse")]
    public class DeleteCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses";

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _instructorId;
        private string _courseToDeleteId;
        private HttpResponseMessage _response;

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

        [When(@"I delete an invalid course Id")]
        public async Task WhenIDeleteAnInvalidCourseId()
        {
            _courseToDeleteId = "invalidCourseId";

            _response = await _client.DeleteAsync($@"{BaseUrl}/{_courseToDeleteId}");
        }

        [When(@"I delete a course belonging to an other organization")]
        public async Task WhenIDeleteACourseBelongingToAnOtherOrganization()
        {
            var otherOrganization = new Organization("other organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(otherOrganization);

            var otherInstructor = new User("otherinstructor@email.com", otherOrganization.Id);
            _factory.CreateUser(otherInstructor);

            var course = new Course("course", otherInstructor.Id, DateTime.Now);
            _courseToDeleteId = course.Id;
            _factory.CreateCourse(course);

            _response = await _client.DeleteAsync($@"{BaseUrl}/{_courseToDeleteId}");
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Given(@"A course belonging to my organization")]
        public void GivenACourseBelongingToMyOrganization()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _courseToDeleteId = course.Id;
            _factory.CreateCourse(course);
        }

        [When(@"I delete the course")]
        public async Task WhenIDeleteTheCourse()
        {
            _response = await _client.DeleteAsync($@"{BaseUrl}/{_courseToDeleteId}");
        }

        [Then(@"The deleted course Id should be returned")]
        public async Task ThenTheDeletedCourseIdShouldBeReturned()
        {
            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.EqualTo(_courseToDeleteId));
        }

        [Then(@"The course should be deleted")]
        public async Task ThenTheCourseShouldBeDeleted()
        {
            var deletedCourse = await _factory.GetCourseById(_courseToDeleteId);

            Assert.That(deletedCourse, Is.Null);
        }
    }
}