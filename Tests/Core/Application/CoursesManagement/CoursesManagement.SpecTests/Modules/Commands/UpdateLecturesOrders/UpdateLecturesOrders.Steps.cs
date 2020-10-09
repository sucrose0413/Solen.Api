using System;
using System.Collections.Generic;
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
    [Binding, Scope(Tag = "UpdateLecturesOrders")]
    public class UpdateLecturesOrdersSteps
    {
        private const string BaseUrl = "/api/courses-management/modules/lecturesOrders";
        private UpdateLecturesOrdersCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;
        private string[] _lecturesIds;

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

        [When(@"I update a module lectures with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateAModuleLecturesWithAnEmptyOrANullId(string moduleId)
        {
            _command = new UpdateLecturesOrdersCommand {ModuleId = moduleId, LecturesOrders = new LectureOrderDto[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a module lectures without specifying lectures orders")]
        public async Task WhenIUpdateAModuleLecturesWithoutSpecifyingLecturesOrders()
        {
            _command = new UpdateLecturesOrdersCommand
                {ModuleId = "moduleId", LecturesOrders = new List<LectureOrderDto>()};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }


        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update a module lectures with an invalid module Id")]
        public async Task WhenIUpdateAModuleLecturesWithAnInvalidModuleId()
        {
            _command = new UpdateLecturesOrdersCommand
                {ModuleId = "invalidModuleId", LecturesOrders = new LectureOrderDto[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update lectures orders of a published course")]
        public async Task WhenIUpdateLecturesOrdersOfAPublishedCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new UpdateLecturesOrdersCommand {ModuleId = module.Id, LecturesOrders = new LectureOrderDto[1]};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A draft course and a module having the following lectures orders")]
        public void GivenADraftCourseAndAModuleHavingTheFollowingLecturesOrders(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            var lecture1 = new ArticleLecture("lecture 1", module.Id, 1, "lecture 1 content");
            _factory.CreateLecture(lecture1);
            var lecture2 = new VideoLecture("lecture 2", module.Id, 2);
            _factory.CreateLecture(lecture2);

            _command = new UpdateLecturesOrdersCommand {ModuleId = module.Id};
            _lecturesIds = new[] {lecture1.Id, lecture2.Id};
        }

        [When(@"I update the lectures orders as")]
        public async Task WhenIUpdateTheLecturesOrdersAs(Table table)
        {
            _command.LecturesOrders = new[]
            {
                new LectureOrderDto {LectureId = _lecturesIds[0], Order = 2},
                new LectureOrderDto {LectureId = _lecturesIds[1], Order = 1}
            };

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The lectures orders should be successfully updated with the new orders")]
        public async Task ThenTheLecturesOrdersShouldBeSuccessfullyUpdatedWithTheNewOrders()
        {
            _response.EnsureSuccessStatusCode();

            var lecture1 = await _factory.GetLectureById(_lecturesIds[0]);
            Assert.That(lecture1.Order, Is.EqualTo(2));

            var lecture2 = await _factory.GetLectureById(_lecturesIds[1]);
            Assert.That(lecture2.Order, Is.EqualTo(1));
        }
    }
}