using System;
using System.Collections.Generic;
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
    [Binding, Scope(Tag = "UpdateModulesOrders")]
    public class UpdateModulesOrdersSteps
    {
        private const string BaseUrl = "/api/courses-management/courses/modulesOrders";
        private UpdateModulesOrdersCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;
        private string[] _modulesIds;

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

        [When(@"I update a course modules with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateACourseModulesWithAnEmptyOrANullId(string courseId)
        {
            _command = new UpdateModulesOrdersCommand {CourseId = courseId, ModulesOrders = new ModuleOrderDto[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course modules without specifying modules orders")]
        public async Task WhenIUpdateACourseModulesWithoutSpecifyingModulesOrders()
        {
            _command = new UpdateModulesOrdersCommand
                {CourseId = "courseId", ModulesOrders = new List<ModuleOrderDto>()};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }


        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update a course modules with an invalid course Id")]
        public async Task WhenIUpdateACourseModulesWithAnInvalidCourseId()
        {
            _command = new UpdateModulesOrdersCommand
                {CourseId = "invalidCourseId", ModulesOrders = new ModuleOrderDto[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course belonging to an other organization")]
        public async Task WhenIUpdateACourseBelongingToAnOtherOrganization()
        {
            var otherOrganization = new Organization("other organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(otherOrganization);

            var otherInstructor = new User("otherinstructor@email.com", otherOrganization.Id);
            _factory.CreateUser(otherInstructor);

            var course = new Course("course", otherInstructor.Id, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new UpdateModulesOrdersCommand {CourseId = course.Id, ModulesOrders = new ModuleOrderDto[1]};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update modules orders of a published course")]
        public async Task WhenIUpdateModulesOrdersOfAPublishedCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            _command = new UpdateModulesOrdersCommand {CourseId = course.Id, ModulesOrders = new ModuleOrderDto[1]};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A draft course having the following modules orders")]
        public void GivenADraftCourseHavingTheFollowingModulesOrders(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module1 = new Module("module 1", course.Id, 1);
            _factory.CreateModule(module1);
            var module2 = new Module("module 2", course.Id, 2);
            _factory.CreateModule(module2);

            _command = new UpdateModulesOrdersCommand {CourseId = course.Id};
            _modulesIds = new[] {module1.Id, module2.Id};
        }

        [When(@"I update the modules orders as")]
        public async Task WhenIUpdateTheModulesOrdersAs(Table table)
        {
            _command.ModulesOrders = new[]
            {
                new ModuleOrderDto {ModuleId = _modulesIds[0], Order = 2},
                new ModuleOrderDto {ModuleId = _modulesIds[1], Order = 1}
            };

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The modules orders should be successfully updated with the new orders")]
        public async Task ThenTheModulesOrdersShouldBeSuccessfullyUpdatedWithTheNewOrders()
        {
            _response.EnsureSuccessStatusCode();

            var module1 = await _factory.GetModuleById(_modulesIds[0]);
            Assert.That(module1.Order, Is.EqualTo(2));

            var module2 = await _factory.GetModuleById(_modulesIds[1]);
            Assert.That(module2.Order, Is.EqualTo(1));
        }
    }
}