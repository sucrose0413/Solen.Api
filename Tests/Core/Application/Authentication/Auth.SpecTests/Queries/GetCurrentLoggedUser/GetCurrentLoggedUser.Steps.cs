using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace Auth.SpecTests.Queries.GetCurrentLoggedUser
{
    [Binding, Scope(Tag = "GetCurrentLoggedUser")]
    public class GetCurrentLoggedUserSteps
    {
        private const string BaseUrl = "/api/auth/currentUser";

        private AuthTestsFactory _factory;
        private HttpClient _client;
        private User _user;
        private HttpResponseMessage _response;


        [BeforeScenario]
        public void ScenarioSetUp()
        {
            _factory = new AuthTestsFactory();
            _client = _factory.GetAnonymousClient();
        }

        [AfterScenario]
        public void ScenarioTearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [When(@"The system is called by an unauthenticated user")]
        public async Task WhenTheSystemIsCalledByAnUnauthenticatedUser()
        {
            _client = _factory.GetAnonymousClient();

            _response = await _client.GetAsync(BaseUrl);
        }


        [Then(@"The system should return a unauthorised error")]
        public void ThenTheSystemShouldReturnAUnauthorisedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [When(@"The token bearer has been blocked")]
        public async Task WhenTheTokenBearerHasBeenBlocked()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var user = new User("email", organization.Id);
            user.ChangeUserStatus(new BlockedStatus());
            _factory.CreateUser(user);

            _client = _factory.GetAuthenticatedClient(user);
            _response = await _client.GetAsync(BaseUrl);
        }

        [Then(@"The system should return a locked error")]
        public void ThenTheSystemShouldReturnALockedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Locked));
        }

        [Given(@"An active user of the system with the following info bearing a valid token")]
        public void GivenAnActiveUserOfTheSystemWithTheFollowingInfoBearingAValidToken(Table table)
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            _user = new User("user@email.com", organization.Id);
            _user.UpdateUserName("Jean Dupont");
            _user.ChangeUserStatus(new ActiveStatus());
            _factory.CreateUser(_user);

            var learningPath = new LearningPath("Developer", organization.Id);
            _factory.AddLearningPath(learningPath);

            _user.UpdateLearningPath(learningPath);
            _factory.UpdateUser(_user);
        }

        [When(@"He calls the system using the token")]
        public async Task WhenHeCallsTheSystemUsingTheToken()
        {
            _client = _factory.GetAuthenticatedClient(_user);
            _response = await _client.GetAsync(BaseUrl);
        }


        [Then(@"The request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"The user info should be returned as")]
        public async Task ThenTheUserInfoShouldBeReturnedAs(Table table)
        {
            var loggedUserDto = await Utilities.GetResponseContent<LoggedUserDto>(_response);

            Assert.That(loggedUserDto, Is.Not.Null);
            Assert.That(loggedUserDto.Id, Is.EqualTo(_user.Id));
            Assert.That(loggedUserDto.UserName, Is.EqualTo(_user.UserName));
            Assert.That(loggedUserDto.LearningPath, Is.EqualTo(_user.LearningPath.Name));
        }
    }
}