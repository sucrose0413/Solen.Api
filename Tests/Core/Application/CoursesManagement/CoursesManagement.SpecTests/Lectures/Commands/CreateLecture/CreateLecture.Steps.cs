using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application;
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
    [Binding, Scope(Tag = "CreateLecture")]
    public class CreateLectureSteps
    {
        private const string BaseUrl = "/api/courses-management/lectures";
        private CreateLectureCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _createdLectureId;
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

        [When(@"I create a lecture with an empty or a null ""(.*)""")]
        public async Task WhenICreateALectureWithAnEmptyOrANull(string title)
        {
            _command = new CreateLectureCommand {Title = title, ModuleId = "moduleId", LectureType = "Article"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a lecture with a title length over (.*) characters")]
        public async Task WhenICreateALectureWithATitleLengthOverCharacters(int maximumTitleLength)
        {
            _command = new CreateLectureCommand
                {Title = new string('*', maximumTitleLength + 1), ModuleId = "moduleId", LectureType = "Article"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a lecture with an empty or a null module Id ""(.*)""")]
        public async Task ThenICreateALectureWithAnEmptyOrANullModuleId(string moduleId)
        {
            _command = new CreateLectureCommand {Title = "Lecture title", ModuleId = moduleId, LectureType = "Article"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a lecture with an empty or a null type ""(.*)""")]
        public async Task ThenICreateALectureWithAnEmptyOrANullType(string lectureType)
        {
            _command = new CreateLectureCommand
                {Title = "Lecture title", ModuleId = "moduleId", LectureType = lectureType};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I create a lecture with an invalid module Id")]
        public async Task WhenICreateALectureWithAnInvalidModuleId()
        {
            _command = new CreateLectureCommand
                {Title = "Lecture title", ModuleId = "invalidModuleId", LectureType = "Article"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I create a lecture while the course is published")]
        public async Task WhenICreateALectureWhileTheCourseIsPublished()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(new PublishedStatus());
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new CreateLectureCommand
                {Title = "Lecture title", ModuleId = module.Id, LectureType = "Article"};
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"The following valid lecture types")]
        public void GivenTheFollowingValidLectureTypes(Table table)
        {
        }

        [When(@"I create a lecture with an invalid type")]
        public async Task WhenICreateALectureWithAnInvalidType()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(new PublishedStatus());
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new CreateLectureCommand
                {Title = "Lecture title", ModuleId = module.Id, LectureType = "InvalidLectureType"};
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a lecture with valid properties as")]
        public async Task WhenICreateALectureWithValidPropertiesAs(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            var module = new Module("module", course.Id, 1);
            _factory.CreateModule(module);

            _command = new CreateLectureCommand
            {
                Title = "lecture title", ModuleId = module.Id, LectureType = "Article",
                Content = "lecture content", Order = 1, Duration = 60
            };
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The lecture Id should be returned")]
        public async Task ThenTheLectureIdShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();

            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.Not.Null);

            _createdLectureId = commandResponse.Value;
        }

        [Then(@"The lecture should be created")]
        public async Task ThenTheLectureShouldBeCreated()
        {
            var createdLecture = await _factory.GetLectureById(_createdLectureId);

            Assert.That(createdLecture, Is.Not.Null);
            Assert.That(createdLecture.Title, Is.EqualTo(_command.Title));
            Assert.That(createdLecture.ModuleId, Is.EqualTo(_command.ModuleId));
            Assert.That(createdLecture.LectureType.Name, Is.EqualTo(_command.LectureType));
            Assert.That(createdLecture.Duration, Is.EqualTo(_command.Duration));
            Assert.That(createdLecture.Order, Is.EqualTo(_command.Order));
            Assert.That((createdLecture as ArticleLecture)?.Content, Is.EqualTo(_command.Content));
        }
    }
}