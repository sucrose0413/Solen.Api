using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
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
    [Binding, Scope(Tag = "CreateModule")]
    public class CreateModuleSteps
    {
        private const string BaseUrl = "/api/courses-management/modules";
        private CreateModuleCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _createdModuleId;
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

        [When(@"I create a module with an empty or a null ""(.*)""")]
        public async Task WhenICreateAModuleWithAnEmptyOrANull(string name)
        {
            _command = new CreateModuleCommand {Name = name, CourseId = "courseId"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a module with a name length over (.*) characters")]
        public async Task WhenICreateAModuleWithANameLengthOverCharacters(int maximumNameLength)
        {
            _command = new CreateModuleCommand {Name = new string('*', maximumNameLength + 1), CourseId = "courseId"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a module with an empty or a null course Id ""(.*)""")]
        public async Task ThenICreateAModuleWithAnEmptyOrANullCourseId(string courseId)
        {
            _command = new CreateModuleCommand {Name = "Module name", CourseId = courseId};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }


        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I create a module with an invalid course Id")]
        public async Task WhenICreateAModuleWithAnInvalidCourseId()
        {
            _command = new CreateModuleCommand {Name = "Module name", CourseId = "invalidCourseId"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I create a module while the course is published")]
        public async Task WhenICreateAModuleWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            _command = new CreateModuleCommand {Name = "Module name", CourseId = course.Id};
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a module with valid properties as")]
        public async Task WhenICreateAModuleWithValidPropertiesAs(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new CreateModuleCommand {Name = "Module name", CourseId = course.Id, Order = 1};
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The module Id should be returned")]
        public async Task ThenTheModuleIdShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();

            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.Not.Null);

            _createdModuleId = commandResponse.Value;
        }

        [Then(@"The module should be created")]
        public async Task ThenTheModuleShouldBeCreated()
        {
            var createdModule = await _factory.GetModuleById(_createdModuleId);

            Assert.That(createdModule, Is.Not.Null);
            Assert.That(createdModule.Name, Is.EqualTo(_command.Name));
            Assert.That(createdModule.CourseId, Is.EqualTo(_command.CourseId));
            Assert.That(createdModule.Order, Is.EqualTo(_command.Order));
        }
    }
}