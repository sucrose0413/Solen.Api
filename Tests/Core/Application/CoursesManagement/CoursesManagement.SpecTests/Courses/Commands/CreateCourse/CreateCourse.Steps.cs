using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Courses.Commands
{
    [Binding, Scope(Tag = "CreateCourse")]
    public class CreateCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses";
        private CreateCourseCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private string _createdCourseId;
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
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I create a course with an empty or a null ""(.*)""")]
        public async Task WhenICreateACourseWithAnEmptyOrANull(string title)
        {
            _command = new CreateCourseCommand {Title = title};
            
            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I create a course with a title length over (.*) characters")]
        public async Task WhenICreateACourseWithATitleLengthOverCharacters(int maximumTitleLength)
        {
            _command = new CreateCourseCommand {Title = new string('*', maximumTitleLength + 1)};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [When(@"I create a course with a valid title")]
        public async Task WhenICreateACourseWithAValidTitle()
        {
            _command = new CreateCourseCommand {Title = "new course"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The course Id should be returned")]
        public async Task ThenTheCourseIdShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();

            var commandResponse = await Utilities.GetResponseContent<CommandResponse>(_response);

            Assert.That(commandResponse, Is.Not.Null);
            Assert.That(commandResponse.Value, Is.Not.Null);

            _createdCourseId = commandResponse.Value;
        }

        [Then(@"The course should be created")]
        public async Task ThenTheCourseShouldBeCreated()
        {
            var createdCourse = await _factory.GetCourseById(_createdCourseId);

            Assert.That(createdCourse, Is.Not.Null);
            Assert.That(createdCourse.Title, Is.EqualTo(_command.Title));
        }
    }
}