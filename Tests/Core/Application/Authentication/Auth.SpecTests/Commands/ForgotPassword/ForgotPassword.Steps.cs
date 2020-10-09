using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace Auth.SpecTests.Commands
{
    [Binding, Scope(Tag = "ForgotPassword")]
    public class ForgotPasswordSteps
    {
        private const string BaseUrl = "/api/auth/forgotPassword";
        private ForgotPasswordCommand _command;

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


        [When(@"I submit a forgot password request with an invalid ""([^""]*)""")]
        public async Task WhenISubmitAForgotPasswordRequestWithAnInvalidEmail(string email)
        {
            _command = new ForgotPasswordCommand {Email = email};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I submit a forgot password request with an unknown email")]
        public async Task WhenISubmitAForgotPasswordRequestWithAnUnknownEmail()
        {
            _command = new ForgotPasswordCommand {Email = "unknown@email.com"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Given(@"I am a blocked user")]
        public void GivenIAmABlockedUser()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            _user = new User("user@email.com", organization.Id);
            _user.ChangeUserStatus(BlockedStatus.Instance);;
            _factory.CreateUser(_user);
        }

        [When(@"I submit a forgot password request")]
        public async Task WhenISubmitAForgotPasswordRequest()
        {
            _command = new ForgotPasswordCommand {Email = _user.Email};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get locked error")]
        public void ThenIShouldGetLockedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Locked));
        }

        [Given(@"I am an active user of the system")]
        public void GivenIAmAnActiveUserOfTheSystem()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            _user = new User("user@email.com", organization.Id);
            _user.ChangeUserStatus(ActiveStatus.Instance);
            _factory.CreateUser(_user);
        }

        [Then(@"The request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }
        
        [Then(@"A password rest token associated to the user should be created")]
        public async Task ThenAPasswordRestTokenAssociatedToTheUserShouldBeCreated()
        {
            var user = await _factory.GetUserById(_user.Id);
            
            Assert.That(user.PasswordToken, Is.Not.Null);
        }
    }
}