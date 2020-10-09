using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
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
    [Binding, Scope(Tag = "UpdateModule")]
    public class UpdateModuleSteps
    {
        private const string BaseUrl = "/api/courses-management/modules";
        private UpdateModuleCommand _command;

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

        [When(@"I update a module with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateAModuleWithAnEmptyOrANullId(string moduleId)
        {
            _command = new UpdateModuleCommand {ModuleId = moduleId, Name = "module name"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a module with an empty or a null name ""(.*)""")]
        public async Task WhenIUpdateAModuleWithAnEmptyOrANullTitle(string moduleName)
        {
            _command = new UpdateModuleCommand {ModuleId = "moduleId", Name = moduleName};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a module with a name length over (.*) characters")]
        public async Task WhenIUpdateAModuleWithANameLengthOverCharacters(int maximumNameLength)
        {
            _command = new UpdateModuleCommand
                {ModuleId = "moduleId", Name = new string('*', maximumNameLength + 1)};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update an invalid module Id")]
        public async Task WhenIUpdateAnInvalidModuleId()
        {
            _command = new UpdateModuleCommand {ModuleId = "invalidModuleId", Name = "module name"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update a module while the course is published")]
        public async Task WhenIUpdateAModuleWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new UpdateModuleCommand {ModuleId = module.Id, Name = "module name"};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A module belonging to a draft course with")]
        public void GivenAModuleBelongingToADraftCourseWith(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new UpdateModuleCommand {ModuleId = module.Id};
        }

        [When(@"I update the module with")]
        public async Task WhenIUpdateTheModuleWith(Table table)
        {
            _command.Name = "new name";

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The module should be successfully updated with the new data")]
        public async Task ThenTheModuleShouldBeSuccessfullyUpdatedWithTheNewData()
        {
            _response.EnsureSuccessStatusCode();

            var lecture = await _factory.GetModuleById(_command.ModuleId);

            Assert.That(lecture.Name, Is.EqualTo(_command.Name));
        }
    }
}