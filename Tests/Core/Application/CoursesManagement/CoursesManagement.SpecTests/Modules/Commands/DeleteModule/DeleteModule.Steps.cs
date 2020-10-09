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

namespace CoursesManagement.SpecTests.Modules.Commands
{
    [Binding, Scope(Tag = "DeleteModule")]
    public class DeleteModuleSteps
    {
          private const string BaseUrl = "/api/courses-management/modules";

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _moduleToDeleteId;
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

        [When(@"I delete an invalid module Id")]
        public async Task WhenIDeleteAnInvalidModuleId()
        {
            _moduleToDeleteId = "invalidModuleId";

            _response = await _client.DeleteAsync($@"{BaseUrl}/{_moduleToDeleteId}");
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I delete a module while the course is published")]
        public async Task WhenIDeleteAModuleWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _response = await _client.DeleteAsync($@"{BaseUrl}/{module.Id}");
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Given(@"A module belonging to a draft course")]
        public void GivenAModuleBelongingToADraftCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _moduleToDeleteId = module.Id;
        }

        [When(@"I delete the module")]
        public async Task WhenIModuleTheLecture()
        {
            _response = await _client.DeleteAsync($@"{BaseUrl}/{_moduleToDeleteId}");
        }

        [Then(@"The deleted module Id should be returned")]
        public async Task ThenTheModuleLectureIdShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();

            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.EqualTo(_moduleToDeleteId));
        }

        [Then(@"The module should be deleted")]
        public async Task ThenTheModuleShouldBeDeleted()
        {
            var deletedModule = await _factory.GetModuleById(_moduleToDeleteId);

            Assert.That(deletedModule, Is.Null);
        }
    }
}